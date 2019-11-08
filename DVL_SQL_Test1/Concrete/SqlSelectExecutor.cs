using DVL_SQL_Test1.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using static DVL_SQL_Test1.Helpers.DvlSqldatareaderHelpers;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlSelectExecutor : ISelectExecutable
    {
        private readonly IDvlSqlConnection _connection;
        private readonly SqlSelector _selector;

        public SqlSelectExecutor(IDvlSqlConnection connection, SqlSelector selector) =>
            (this._connection, this._selector) = (connection, selector);

        public async Task<List<TResult>> ToListAsync<TResult>(Func<SqlDataReader, TResult> selectorFunc,
            int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default)
        {
            return await this._connection.ConnectAsync(
                dvlCommand => dvlCommand.ExecuteReaderAsync(ConverterFunc, timeout, behavior, cancellationToken),
                this._selector.GetSqlString(),
                parameters: this._selector.GetDvlSqlParameters().Select(dvlSql => dvlSql.SqlParameter).ToArray());

            List<TResult> ConverterFunc(SqlDataReader reader) => AsList(reader, selectorFunc);
        }

        public async Task<List<TResult>> ToListAsync<TResult>(int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default)
        {
            return await this._connection.ConnectAsync(
                dvlCommand => dvlCommand.ExecuteReaderAsync(ConverterFunc, timeout, behavior, cancellationToken),
                this._selector.GetSqlString(),
                parameters: this._selector.GetDvlSqlParameters().Select(dvlSql => dvlSql.SqlParameter).ToArray());

            static List<TResult> ConverterFunc(SqlDataReader reader) => AsList(reader, r => (TResult)r[0]);
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
                this._selector.WithSelectTop(1).GetSqlString(),
                parameters: this._selector.GetDvlSqlParameters().Select(dvlSql => dvlSql.SqlParameter).ToArray());

            TResult ConverterFunc(SqlDataReader reader) => First(reader, readerFunc);
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
                this._selector.WithSelectTop(1).GetSqlString(),
                parameters: this._selector.GetDvlSqlParameters().Select(dvlSql => dvlSql.SqlParameter).ToArray());

            TResult ConverterFunc(SqlDataReader reader) => FirstOrDefault(reader, readerFunc);
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
                this._selector.GetSqlString(),
                parameters: this._selector.GetDvlSqlParameters().Select(dvlSql => dvlSql.SqlParameter).ToArray());

            TResult ConverterFunc(SqlDataReader reader) => Single(reader, readerFunc);
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
                this._selector.GetSqlString(),
                parameters: this._selector.GetDvlSqlParameters().Select(dvlSql => dvlSql.SqlParameter).ToArray());

            TResult ConverterFunc(SqlDataReader reader) => SingleOrDefault(reader, readerFunc);
        }
    }
}
