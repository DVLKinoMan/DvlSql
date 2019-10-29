namespace DVL_SQL_Test1.Abstract
{
    public interface IOrderable
    {
        IExecutor Select(params string[] parameterNames);
        IExecutor Select();
        IExecutor SelectTop(int count, params string[] parameterNames);
        IOrderable OrderBy(params string[] fields);
        IOrderable OrderByDescending(params string[] fields);
    }
}
