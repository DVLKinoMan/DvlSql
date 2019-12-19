using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlOrderer : IOrderer
    {
        private readonly SqlSelector _selector;
        private readonly ISelectExecutable _executor;

        public SqlOrderer(SqlSelector selector, ISelectExecutable executor) => (this._selector, this._executor) = (selector, executor);

        public IOrderer OrderBy(params string[] fields) => this._selector.OrderBy(this, fields);

        public IOrderer OrderByDescending(params string[] fields) => this._selector.OrderByDescending(this, fields);

        public Task<List<TResult>> ToListAsync<TResult>(Func<SqlDataReader, TResult> reader, int? timeout = null,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default) =>
            this._executor.ToListAsync(reader, timeout, behavior, cancellationToken);

        public Task<List<TResult>> ToListAsync<TResult>(int? timeout = null,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default) =>
            this._executor.ToListAsync<TResult>(timeout, behavior, cancellationToken);

        public Task<Dictionary<TKey, List<TValue>>> ToDictionaryAsync<TKey, TValue>(
            Func<SqlDataReader, TKey> keySelector, Func<SqlDataReader, TValue> valueSelector, int? timeout = null,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default) =>
            this._executor.ToDictionaryAsync(keySelector, valueSelector, timeout, behavior, cancellationToken);
    
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
