namespace Dvl_Sql.Abstract
{
    public interface IFromable
    {
        ISelector From(string tableName, bool withNoLock = false);
    }
}