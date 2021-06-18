using System.Threading;
using System.Threading.Tasks;

namespace DvlSql.Abstract
{
    public interface IInsertDeleteExecutable
    {
        Task<int> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default);
    }
}
