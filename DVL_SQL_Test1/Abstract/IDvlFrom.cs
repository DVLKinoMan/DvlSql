namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlFrom
    {
        IDvlSelect From(string tableName, bool withNoLock = false);
    }
}
