using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DvlSql.Abstract
{
    public interface IDvlSqlConnection : IDisposable, IAsyncDisposable
    {
        Task<TResult> ConnectAsync<TResult>(Func<IDvlSqlCommand, Task<TResult>> func, string sqlString,
            CommandType commandType = CommandType.Text, params SqlParameter[] parameters);

        ValueTask<DbTransaction> BeginTransactionAsync(CancellationToken token = default);

        Task CommitAsync(CancellationToken token = default);

        Task RollbackAsync(CancellationToken token = default);

        IDvlSqlConnection GetClone();
    }
}
