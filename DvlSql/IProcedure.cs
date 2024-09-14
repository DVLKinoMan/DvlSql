namespace DvlSql;

public interface IProcedure
{
    IProcedureExecutable Procedure(string procedureName, params DvlSqlParameter[] parameters);
}
