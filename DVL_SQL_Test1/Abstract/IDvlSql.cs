using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlSql
    {
        ISelector From(string tableName, bool withNoLock = false);
        IInsertable<TRes> InsertInto<TRes>(string tableName, IEnumerable<string> cols) where TRes : ITuple;
        IInsertable InsertInto(string tableName, IEnumerable<string> cols);
        IDeletable DeleteFrom(string tableName);
        IUpdateSetable Update(string tableName);
    }
}
