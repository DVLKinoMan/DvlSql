using DVL_SQL_Test1.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlExecutor : IExecutor
    {
        private readonly IDvlSqlConnection _connection;
        private readonly DvlSqlSelectable _selectable;
        private DvlSqlOrderByExpression _sqlOrderByExpression;

        public SqlExecutor(IDvlSqlConnection connection, DvlSqlSelectable selectable) =>
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
                this._selectable.GetSqlString(this._sqlOrderByExpression));

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
                this._selectable.GetSqlString(this._sqlOrderByExpression));

            static List<TResult> ConverterFunc(SqlDataReader reader)
            {
                var list = new List<TResult>();
                while (reader.Read())
                    list.Add((TResult) reader[0]);

                return list;
            }
        }

        public async Task<TResult> First<TResult>(int? timeout = default, CancellationToken cancellationToken = default)
        {
            return await First(ConverterFunc, timeout, cancellationToken);

            static TResult ConverterFunc(SqlDataReader reader) => reader[0] is TResult res ? res : throw new ArgumentException("TResult");
        }

        public async Task<TResult> First<TResult>(Func<SqlDataReader, TResult> readerFunc, int? timeout = default, CancellationToken cancellationToken = default)
        {
            return await this._connection.ConnectAsync(
                dvlCommand =>
                    dvlCommand.ExecuteReaderAsync(ConverterFunc, timeout, cancellationToken: cancellationToken),
                this._selectable.WithSelectTop(1).GetSqlString(this._sqlOrderByExpression));

            TResult ConverterFunc(SqlDataReader reader) => reader.Read() ? readerFunc(reader) : throw new InvalidOperationException("There was no element in sequence");
        }

        public async Task<TResult> FirstOrDefault<TResult>(int? timeout = default,
            CancellationToken cancellationToken = default)
        {
            return await FirstOrDefault(ConverterFunc, timeout, cancellationToken);

            static TResult ConverterFunc(SqlDataReader reader) => reader[0] is TResult res ? res : throw new ArgumentException("TResult");
        }

        public async Task<TResult> FirstOrDefault<TResult>(Func<SqlDataReader, TResult> readerFunc,
            int? timeout = default, CancellationToken cancellationToken = default)
        {
            return await this._connection.ConnectAsync(
                dvlCommand =>
                    dvlCommand.ExecuteReaderAsync(ConverterFunc, timeout, cancellationToken: cancellationToken),
                this._selectable.WithSelectTop(1).GetSqlString(this._sqlOrderByExpression));

            TResult ConverterFunc(SqlDataReader reader) => reader.Read() ? readerFunc(reader) : default;
        }

        public IExecutor OrderBy(params string[] fields)
        {
            if (this._sqlOrderByExpression == null)
                this._sqlOrderByExpression = new DvlSqlOrderByExpression(fields.Select(f => (f, Ascending: Ordering.ASC)));
            else this._sqlOrderByExpression.AddRange(fields.Select(f => (f, Ascending: Ordering.ASC)));

            return this;
        }

        public IExecutor OrderByDescending(params string[] fields)
        {
            if (this._sqlOrderByExpression == null)
                this._sqlOrderByExpression = new DvlSqlOrderByExpression(fields.Select(f => (f, Descending: Ordering.DESC)));
            else this._sqlOrderByExpression.AddRange(fields.Select(f => (f, Descending: Ordering.DESC)));

            return this;
        }

        public async Task<TResult> Single<TResult>(int? timeout = null, CancellationToken cancellationToken = default)
        {
            return await Single(ConverterFunc, timeout, cancellationToken);

            static TResult ConverterFunc(SqlDataReader reader) => reader[0] is TResult res ? res : throw new ArgumentException("TResult");
        }

        public async Task<TResult> Single<TResult>(Func<SqlDataReader, TResult> readerFunc, int? timeout = null, CancellationToken cancellationToken = default)
        {
            return await this._connection.ConnectAsync(
                dvlCommand =>
                    dvlCommand.ExecuteReaderAsync(ConverterFunc, timeout, cancellationToken: cancellationToken),
                this._selectable.GetSqlString(this._sqlOrderByExpression));

            TResult ConverterFunc(SqlDataReader reader) => reader.RecordsAffected == 1 && reader.Read() ? readerFunc(reader) : throw new InvalidOperationException("There was no element in sequence or there was more than 1 elements");
        }

        public async Task<TResult> SingleOrDefault<TResult>(int? timeout = null, CancellationToken cancellationToken = default)
        {
            return await SingleOrDefault(ConverterFunc, timeout, cancellationToken);

            static TResult ConverterFunc(SqlDataReader reader) => reader[0] is TResult res ? res : throw new ArgumentException("TResult");
        }

        public async Task<TResult> SingleOrDefault<TResult>(Func<SqlDataReader, TResult> readerFunc, int? timeout = null, CancellationToken cancellationToken = default)
        {
            return await this._connection.ConnectAsync(
                dvlCommand =>
                    dvlCommand.ExecuteReaderAsync(ConverterFunc, timeout, cancellationToken: cancellationToken),
                this._selectable.GetSqlString(this._sqlOrderByExpression));

            TResult ConverterFunc(SqlDataReader reader) => reader.RecordsAffected == 1 && reader.Read() ? readerFunc(reader) : default;
        }
    }
}
