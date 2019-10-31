namespace DVL_SQL_Test1.Abstract
{
    // ReSharper disable once IdentifierTypo
    public interface IOrderable
    {
        IOrderer OrderBy(params string[] fields);
        IOrderer OrderByDescending(params string[] fields);
    }
}
