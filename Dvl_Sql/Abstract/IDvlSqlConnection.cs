using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Dvl_Sql.Abstract
{
    public interface IDvlSqlConnection : IDisposable
    {
        Task<TResult> ConnectAsync<TResult>(Func<IDvlSqlCommand, Task<TResult>> func, string sqlString,
            CommandType commandType = CommandType.Text, params SqlParameter[] parameters);
    }
}
