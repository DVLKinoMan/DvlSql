using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dvl_Sql.Abstract
{
    public interface ITransaction : ICommitable
    {
        Task<ICommitable> BeginTransactionAsync(Action action, CancellationToken token = default);
        Task BeginTransactionAsync(CancellationToken token = default);
    }
}
