namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlWhere
    {
        IDvlSqlExecutor Select(params string[] parameterNames);
        IDvlSqlExecutor Select();
        IDvlSqlExecutor SelectTop(int count, params string[] parameterNames);
        IDvlGroupBy GroupBy(params string[] parameterNames);
    }
}
