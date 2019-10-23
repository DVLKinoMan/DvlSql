using System.Threading;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlSqlCommand<TResult>
    {
        Task<TResult> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default);
    }
}
