using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlOrderBy : IDvlOrderBy
    {
        private readonly DvlSelect _selectable;
        private readonly IDvlSqlExecutor _executor;

        public DvlOrderBy(DvlSelect selectable, IDvlSqlExecutor executor) => (this._selectable, this._executor) = (selectable, executor);

        public IDvlOrderBy OrderBy(params string[] fields) => this._selectable.OrderBy(this, fields);

        public IDvlOrderBy OrderByDescending(params string[] fields) => this._selectable.OrderByDescending(this, fields);

        public (int, bool) Execute() => this._executor.Execute();

        public Task<List<TResult>> ToListAsync<TResult>(Func<SqlDataReader, TResult> reader, int? timeout = null,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default) =>
            this._executor.ToListAsync(reader, timeout, behavior, cancellationToken);

        public Task<List<TResult>> ToListAsync<TResult>(int? timeout = null,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default) =>
            this._executor.ToListAsync<TResult>(timeout, behavior, cancellationToken);

        public Task<TResult> FirstAsync<TResult>(int? timeout = null, CancellationToken cancellationToken = default) =>
            this._executor.FirstAsync<TResult>(timeout, cancellationToken);

        public Task<TResult> FirstAsync<TResult>(Func<SqlDataReader, TResult> reader, int? timeout = null,
            CancellationToken cancellationToken = default) =>
            this._executor.FirstAsync(reader, timeout, cancellationToken);

        public Task<TResult> SingleAsync<TResult>(int? timeout = null, CancellationToken cancellationToken = default) =>
            this._executor.SingleAsync<TResult>(timeout, cancellationToken);

        public Task<TResult> SingleAsync<TResult>(Func<SqlDataReader, TResult> reader, int? timeout = null,
            CancellationToken cancellationToken = default) =>
            this._executor.SingleAsync(reader, timeout, cancellationToken);

        public Task<TResult> SingleOrDefaultAsync<TResult>(int? timeout = null,
            CancellationToken cancellationToken = default) =>
            this._executor.SingleOrDefaultAsync<TResult>(timeout, cancellationToken);

        public Task<TResult> SingleOrDefaultAsync<TResult>(Func<SqlDataReader, TResult> reader, int? timeout = null,
            CancellationToken cancellationToken = default) =>
            this._executor.SingleOrDefaultAsync(reader, timeout, cancellationToken);

        public Task<TResult> FirstOrDefaultAsync<TResult>(int? timeout = null,
            CancellationToken cancellationToken = default) =>
            this._executor.FirstOrDefaultAsync<TResult>(timeout, cancellationToken);

        public Task<TResult> FirstOrDefaultAsync<TResult>(Func<SqlDataReader, TResult> reader, int? timeout = null,
            CancellationToken cancellationToken = default) =>
            this._executor.FirstOrDefaultAsync(reader, timeout, cancellationToken);

    }
}
