using System.Data;
using System.Data.SqlClient;
using Dvl_Sql.Abstract;

namespace Dvl_Sql.Concrete
{
    internal class DvlMSSqlCommandFactory : IDvlMsSqlCommandFactory
    {
        public IDvlSqlCommand CreateSqlCommand(CommandType commandType, SqlConnection connection, string sqlString,
            params SqlParameter[] parameters)
        {
            var command = new SqlCommand(sqlString, connection)
            {
                CommandType = commandType
            };

            if(parameters!=null)
                foreach (var parameter in parameters)
                    command.Parameters.Add(parameter);

            return new DvlSqlCommand(command);
        }
    }
}