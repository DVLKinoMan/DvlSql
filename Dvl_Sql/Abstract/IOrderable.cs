namespace Dvl_Sql.Abstract
{
    // ReSharper disable once IdentifierTypo
    public interface IOrderable
    {
        IOrderExecutable OrderBy(params string[] fields);
        IOrderExecutable OrderByDescending(params string[] fields);
    }
}
