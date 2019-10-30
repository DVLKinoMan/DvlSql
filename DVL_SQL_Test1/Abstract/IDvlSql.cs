namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlSql
    {
        ISelector From(string tableName, bool withNoLock = false);
    }
}
