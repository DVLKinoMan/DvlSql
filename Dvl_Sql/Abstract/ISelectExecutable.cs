using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dvl_Sql.Expressions;
using Dvl_Sql.Models;

namespace Dvl_Sql.Abstract
{
    public interface ISelectExecutable
    {
        Task<List<TResult>> ToListAsync<TResult>(Func<IDataReader, TResult> reader, int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default);

        Task<List<TResult>> ToListAsync<TResult>(int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default);

        Task<Dictionary<TKey, List<TValue>>> ToDictionaryAsync<TKey, TValue>(
            Func<IDataReader, TKey> keySelector,
            Func<IDataReader, TValue> valueSelector,
            int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default);

        Task<TResult> FirstAsync<TResult>(int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> FirstAsync<TResult>(Func<IDataReader, TResult> reader, int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> SingleAsync<TResult>(int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> SingleAsync<TResult>(Func<IDataReader, TResult> reader, int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> SingleOrDefaultAsync<TResult>(int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> SingleOrDefaultAsync<TResult>(Func<IDataReader, TResult> reader, int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> FirstOrDefaultAsync<TResult>(int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> FirstOrDefaultAsync<TResult>(Func<IDataReader, TResult> reader, int? timeout = default, CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(int? timeout = default, CancellationToken cancellationToken = default);

        Task<bool> AllAsync(int? timeout = default, CancellationToken cancellationToken = default);
    }
}
