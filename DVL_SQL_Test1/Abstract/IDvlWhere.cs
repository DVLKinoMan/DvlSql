namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlWhere
    {
        IDvlOrderBy OrderBy(params string[] fields);
        IDvlOrderBy OrderByDescending(params string[] fields);
        IDvlSqlExecutor Select(params string[] parameterNames);
        IDvlSqlExecutor Select();
        IDvlSqlExecutor SelectTop(int count, params string[] parameterNames);
    }
}
