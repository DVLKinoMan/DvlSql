using System.Threading;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Abstract
{
    public interface IInsertExecutable
    {
        Task<int> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default);
    }
}
