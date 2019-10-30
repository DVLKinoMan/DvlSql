namespace DVL_SQL_Test1.Abstract
{
    public interface IFilter
    {
        IExecutor Select(params string[] parameterNames);
        IExecutor Select();
        IExecutor SelectTop(int count, params string[] parameterNames);
        IGrouper GroupBy(params string[] parameterNames);
    }
}
