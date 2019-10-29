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

        Task<TResult> First<TResult>(int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> First<TResult>(Func<SqlDataReader, TResult> reader, int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> Single<TResult>(int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> Single<TResult>(Func<SqlDataReader, TResult> reader, int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> SingleOrDefault<TResult>(int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> SingleOrDefault<TResult>(Func<SqlDataReader, TResult> reader, int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> FirstOrDefault<TResult>(int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> FirstOrDefault<TResult>(Func<SqlDataReader, TResult> reader, int? timeout = default, CancellationToken cancellationToken = default);
        
        IExecutor OrderBy(params string[] fields);
        
        IExecutor OrderByDescending(params string[] fields);
    }
}
