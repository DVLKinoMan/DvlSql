namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlGroupBy
    {
        IDvlSqlExecutor Select(params string[] parameterNames);
        IDvlSqlExecutor Select();
        IDvlSqlExecutor SelectTop(int count, params string[] parameterNames);
        IDvlOrderBy OrderBy(params string[] fields);
        IDvlOrderBy OrderByDescending(params string[] fields);
    }
}
