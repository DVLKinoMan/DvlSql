using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DvlSql.SqlServer
{
    public interface IDvlSqlMsCommandFactory
    {
        IDvlSqlCommand CreateSqlCommand(CommandType commandType, SqlConnection connection, 
            string sqlString, DbTransaction transaction = null, params SqlParameter[] parameters);
    }
}