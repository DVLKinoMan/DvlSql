using System.Threading;

namespace DVL_SQL_Test1.Abstract
{
    public interface IInsertExecutable
    {
        (int, bool) ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default);
    }
}
