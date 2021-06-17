using System.Threading;
using System.Threading.Tasks;

namespace Dvl_Sql.Abstract
{
    public interface ICommitable
    {
        Task CommitAsync(CancellationToken token = default);
    }
}
