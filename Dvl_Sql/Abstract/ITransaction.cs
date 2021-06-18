using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dvl_Sql.Abstract
{
    public interface ITransaction
    {
        Task BeginTransactionAsync(CancellationToken token = default);
        Task CommitAsync(CancellationToken token = default);
        Task RollbackAsync(CancellationToken token = default);
    }
}
