using Dvl_Sql.Models;

namespace Dvl_Sql.Abstract
{
    public interface IProcedure
    {
        IProcedureExecutable Procedure(string procedureName, params DvlSqlParameter[] parameters);
    }
}
