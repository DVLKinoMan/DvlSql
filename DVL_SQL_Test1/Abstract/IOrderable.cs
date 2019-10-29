namespace DVL_SQL_Test1.Abstract
{
    public interface IOrderable
    {
        IExecutor Select(int? topNum = null, params string[] parameterNames);
        IExecutor Select(int? topNum = null);
        IOrderable OrderBy(params string[] fields);
        IOrderable OrderByDescending(params string[] fields);
    }
}
