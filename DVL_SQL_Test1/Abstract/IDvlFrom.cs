namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlFrom
    {
        IDvlSelectable From(string tableName, bool withNoLock = false);
    }
}
