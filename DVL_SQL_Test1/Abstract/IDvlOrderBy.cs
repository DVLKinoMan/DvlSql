namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlOrderBy : IDvlSqlExecutor
    {
        IDvlOrderBy OrderBy(params string[] fields);
        IDvlOrderBy OrderByDescending(params string[] fields);
    }
}
