using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dvl_Sql.Abstract;
using static Dvl_Sql.Helpers.DvlSqlDataReaderHelpers;

namespace Dvl_Sql.Concrete
{
    public class SqlSelectExecutor : ISelectExecutable
    {
        private readonly IDvlSqlConnection _connection;
        private readonly SqlSelector _selector;

        public SqlSelectExecutor(IDvlSqlConnection connection, SqlSelector selector) =>
            (this._connection, this._selector) = (connection, selector);

        public async Task<List<TResult>> ToListAsync<TResult>(Func<SqlDataReader, TResult> selectorFunc,
            int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default) =>
            await this._connection.ConnectAsync(
                dvlCommand => dvlCommand.ExecuteReaderAsync(AsList(selectorFunc), timeout, behavior, cancellationToken),
                this._selector.GetSqlString(),
                parameters: this._selector.GetDvlSqlParameters()?.Select(dvlSql => dvlSql.SqlParameter).ToArray());

        public async Task<List<TResult>> ToListAsync<TResult>(int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default) =>
            await this._connection.ConnectAsync(
                dvlCommand => dvlCommand.ExecuteReaderAsync(AsList(r => (TResult)r[0]), timeout, behavior, cancellationToken),
                this._selector.GetSqlString(),
                parameters: this._selector.GetDvlSqlParameters()?.Select(dvlSql => dvlSql.SqlParameter).ToArray());

        public async Task<Dictionary<TKey, List<TValue>>> ToDictionaryAsync<TKey, TValue>(
            Func<SqlDataReader, TKey> keySelector,
            Func<SqlDataReader, TValue> valueSelector,
            int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default) =>
            await this._connection.ConnectAsync(
                dvlCommand => dvlCommand.ExecuteReaderAsync(AsDictionary(keySelector, valueSelector), timeout, behavior,
                    cancellationToken),
                this._selector.GetSqlString(),
                parameters: this._selector.GetDvlSqlParameters()?.Select(dvlSql => dvlSql.SqlParameter).ToArray());

        public async Task<TResult> FirstAsync<TResult>(int? timeout = default,
            CancellationToken cancellationToken = default) =>
            await FirstAsync(reader => reader[0] is TResult res ? res : throw new ArgumentException("TResult"),
                timeout, cancellationToken);

        public async Task<TResult> FirstAsync<TResult>(Func<SqlDataReader, TResult> readerFunc, int? timeout = default,
            CancellationToken cancellationToken = default) =>
            await this._connection.ConnectAsync(
                dvlCommand =>
                    dvlCommand.ExecuteReaderAsync(First(readerFunc), timeout, cancellationToken: cancellationToken),
                this._selector.WithSelectTop(1).GetSqlString(),
                parameters: this._selector.GetDvlSqlParameters()?.Select(dvlSql => dvlSql.SqlParameter).ToArray());

        public async Task<TResult> FirstOrDefaultAsync<TResult>(int? timeout = default,
            CancellationToken cancellationToken = default) =>
            await FirstOrDefaultAsync(
                reader => reader[0] is TResult res ? res : throw new ArgumentException("TResult"), timeout,
                cancellationToken);

        public async Task<TResult> FirstOrDefaultAsync<TResult>(Func<SqlDataReader, TResult> readerFunc,
            int? timeout = default, CancellationToken cancellationToken = default) =>
            await this._connection.ConnectAsync(
                dvlCommand =>
                    dvlCommand.ExecuteReaderAsync(FirstOrDefault(readerFunc), timeout, cancellationToken: cancellationToken),
                this._selector.WithSelectTop(1).GetSqlString(),
                parameters: this._selector.GetDvlSqlParameters()?.Select(dvlSql => dvlSql.SqlParameter).ToArray());

        public async Task<TResult>
            SingleAsync<TResult>(int? timeout = null, CancellationToken cancellationToken = default) =>
            await SingleAsync(reader => reader[0] is TResult res ? res : throw new ArgumentException("TResult"),
                timeout, cancellationToken);

        public async Task<TResult> SingleAsync<TResult>(Func<SqlDataReader, TResult> readerFunc, int? timeout = null,
            CancellationToken cancellationToken = default) =>
            await this._connection.ConnectAsync(
                dvlCommand =>
                    dvlCommand.ExecuteReaderAsync(Single(readerFunc), timeout, cancellationToken: cancellationToken),
                this._selector.GetSqlString(),
                parameters: this._selector.GetDvlSqlParameters()?.Select(dvlSql => dvlSql.SqlParameter).ToArray());

        public async Task<TResult> SingleOrDefaultAsync<TResult>(int? timeout = null,
            CancellationToken cancellationToken = default) =>
            await SingleOrDefaultAsync(
                reader => reader[0] is TResult res ? res : throw new ArgumentException("TResult"), timeout,
                cancellationToken);

        public async Task<TResult> SingleOrDefaultAsync<TResult>(Func<SqlDataReader, TResult> readerFunc,
            int? timeout = null, CancellationToken cancellationToken = default) =>
            await this._connection.ConnectAsync(
                dvlCommand =>
                    dvlCommand.ExecuteReaderAsync(SingleOrDefault(readerFunc), timeout,
                        cancellationToken: cancellationToken),
                this._selector.GetSqlString(),
                parameters: this._selector.GetDvlSqlParameters()?.Select(dvlSql => dvlSql.SqlParameter).ToArray());
    }
}
