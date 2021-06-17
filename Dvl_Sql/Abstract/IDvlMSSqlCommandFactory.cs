using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dvl_Sql.Abstract
{
    public interface IDvlMsSqlCommandFactory
    {
        IDvlSqlCommand CreateSqlCommand(CommandType commandType, SqlConnection connection, 
            string sqlString, DbTransaction transaction = null, params SqlParameter[] parameters);
    }
}