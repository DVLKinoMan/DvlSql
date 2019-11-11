using DVL_SQL_Test1.Models;

namespace DVL_SQL_Test1.Abstract
{
    public interface IProcedure
    {
        IProcedureExecutable Procedure(string procedureName, params DvlSqlParameter[] parameters);
    }
}
