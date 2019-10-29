namespace DVL_SQL_Test1.Abstract
{
    public interface IWhereable
    {
        IOrderable OrderBy(params string[] fields);
        IOrderable OrderByDescending(params string[] fields);
        IExecutor Select(params string[] parameterNames);
        IExecutor Select();
        IExecutor SelectTop(int count, params string[] parameterNames);
    }
}
