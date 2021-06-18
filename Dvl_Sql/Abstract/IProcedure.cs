using DvlSql.Models;

namespace DvlSql.Abstract
{
    public interface IProcedure
    {
        IProcedureExecutable Procedure(string procedureName, params DvlSqlParameter[] parameters);
    }
}
