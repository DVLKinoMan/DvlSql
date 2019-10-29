using DVL_SQL_Test1.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSqlExecutor : IDvlSqlExecutor
    {
        private readonly IDvlSqlConnection _connection;
        private readonly DvlSelect _selectable;

        public DvlSqlExecutor(IDvlSqlConnection connection, DvlSelect selectable) =>
            (this._connection, this._selectable) = (connection, selectable);

        public (int, bool) Execute()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TResult>> ToListAsync<TResult>(Func<SqlDataReader, TResult> selectorFunc,
            int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default)
        {
            return await this._connection.ConnectAsync(
                dvlCommand => dvlCommand.ExecuteReaderAsync(ConverterFunc, timeout, behavior, cancellationToken),
                this._selectable.GetSqlString());

            List<TResult> ConverterFunc(SqlDataReader reader)
            {
                var list = new List<TResult>();
                while (reader.Read())
                    list.Add(selectorFunc(reader));

                return list;
            }
        }

        public async Task<List<TResult>> ToListAsync<TResult>(int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default)
        {
            return await this._connection.ConnectAsync(
                dvlCommand => dvlCommand.ExecuteReaderAsync(ConverterFunc, timeout, behavior, cancellationToken),
                this._selectable.GetSqlString());

            static List<TResult> ConverterFunc(SqlDataReader reader)
            {
                var list = new List<TResult>();
                while (reader.Read())
                    list.Add((TResult) reader[0]);

                return list;
            }
        }

        public async Task<TResult> FirstAsync<TResult>(int? timeout = default, CancellationToken cancellationToken = default)
        {
            return await FirstAsync(ConverterFunc, timeout, cancellationToken);

            static TResult ConverterFunc(SqlDataReader reader) => reader[0] is TResult res ? res : throw new ArgumentException("TResult");
        }

        public async Task<TResult> FirstAsync<TResult>(Func<SqlDataReader, TResult> readerFunc, int? timeout = default, CancellationToken cancellationToken = default)
        {
            return await this._connection.ConnectAsync(
                dvlCommand =>
                    dvlCommand.ExecuteReaderAsync(ConverterFunc, timeout, cancellationToken: cancellationToken),
                this._selectable.WithSelectTop(1).GetSqlString());

            TResult ConverterFunc(SqlDataReader reader) => reader.Read() ? readerFunc(reader) : throw new InvalidOperationException("There was no element in sequence");
        }

        public async Task<TResult> FirstOrDefaultAsync<TResult>(int? timeout = default,
            CancellationToken cancellationToken = default)
        {
            return await FirstOrDefaultAsync(ConverterFunc, timeout, cancellationToken);

            static TResult ConverterFunc(SqlDataReader reader) => reader[0] is TResult res ? res : throw new ArgumentException("TResult");
        }

        public async Task<TResult> FirstOrDefaultAsync<TResult>(Func<SqlDataReader, TResult> readerFunc,
            int? timeout = default, CancellationToken cancellationToken = default)
        {
            return await this._connection.ConnectAsync(
                dvlCommand =>
                    dvlCommand.ExecuteReaderAsync(ConverterFunc, timeout, cancellationToken: cancellationToken),
                this._selectable.WithSelectTop(1).GetSqlString());

            TResult ConverterFunc(SqlDataReader reader) => reader.Read() ? readerFunc(reader) : default;
        }

        public async Task<TResult> SingleAsync<TResult>(int? timeout = null, CancellationToken cancellationToken = default)
        {
            return await SingleAsync(ConverterFunc, timeout, cancellationToken);

            static TResult ConverterFunc(SqlDataReader reader) => reader[0] is TResult res ? res : throw new ArgumentException("TResult");
        }

        public async Task<TResult> SingleAsync<TResult>(Func<SqlDataReader, TResult> readerFunc, int? timeout = null, CancellationToken cancellationToken = default)
        {
            return await this._connection.ConnectAsync(
                dvlCommand =>
                    dvlCommand.ExecuteReaderAsync(ConverterFunc, timeout, cancellationToken: cancellationToken),
                this._selectable.GetSqlString());

            TResult ConverterFunc(SqlDataReader reader) => IsSingleDataReader(reader, readerFunc) switch
            {
                (true, var value) => value,
                _ => throw new InvalidOperationException(
                    "There was no element in sequence or there was more than 1 elements")
            };
        }

        public async Task<TResult> SingleOrDefaultAsync<TResult>(int? timeout = null, CancellationToken cancellationToken = default)
        {
            return await SingleOrDefaultAsync(ConverterFunc, timeout, cancellationToken);

            static TResult ConverterFunc(SqlDataReader reader) => reader[0] is TResult res ? res : throw new ArgumentException("TResult");
        }

        public async Task<TResult> SingleOrDefaultAsync<TResult>(Func<SqlDataReader, TResult> readerFunc, int? timeout = null, CancellationToken cancellationToken = default)
        {
            return await this._connection.ConnectAsync(
                dvlCommand =>
                    dvlCommand.ExecuteReaderAsync(ConverterFunc, timeout, cancellationToken: cancellationToken),
                this._selectable.GetSqlString());

            TResult ConverterFunc(SqlDataReader reader) => IsSingleDataReader(reader, readerFunc) switch
            {
                (true, var value) => value,
                _ => default
            };
        }

        private (bool isSingle, TResult result) IsSingleDataReader<TResult>(SqlDataReader reader, Func<SqlDataReader, TResult> func)
        {
            if (!reader.Read())
                return (default, default);

            var firstValue = func(reader);
            return reader.Read() ? (false, firstReader: firstValue) : (true, firstReader: firstValue);
        }
    }
}
