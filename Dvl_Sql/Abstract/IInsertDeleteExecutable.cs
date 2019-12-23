using System.Threading;
using System.Threading.Tasks;

namespace Dvl_Sql.Abstract
{
    public interface IInsertDeleteExecutable
    {
        Task<int> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default);
    }
}
