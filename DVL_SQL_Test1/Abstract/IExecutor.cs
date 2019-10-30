using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Abstract
{
    public interface IExecutor
    {
        (int, bool) Execute();

        Task<List<TResult>> ToListAsync<TResult>(Func<SqlDataReader, TResult> reader, int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default);

        Task<List<TResult>> ToListAsync<TResult>(int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default);

        Task<TResult> FirstAsync<TResult>(int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> FirstAsync<TResult>(Func<SqlDataReader, TResult> reader, int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> SingleAsync<TResult>(int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> SingleAsync<TResult>(Func<SqlDataReader, TResult> reader, int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> SingleOrDefaultAsync<TResult>(int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> SingleOrDefaultAsync<TResult>(Func<SqlDataReader, TResult> reader, int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> FirstOrDefaultAsync<TResult>(int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> FirstOrDefaultAsync<TResult>(Func<SqlDataReader, TResult> reader, int? timeout = default, CancellationToken cancellationToken = default);

    }
}
