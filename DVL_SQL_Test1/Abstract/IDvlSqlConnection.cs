using System;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlSqlConnection
    {
        Task<TResult> ConnectAsync<TResult>(Func<IDvlSqlCommand, Task<TResult>> func);
    }
}
