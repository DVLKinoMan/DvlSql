namespace DVL_SQL_Test1.Abstract
{
    public interface IOrderer : IExecutor
    {
        IOrderer OrderBy(params string[] fields);
        IOrderer OrderByDescending(params string[] fields);
    }
}
