using System;
using System.Collections.Generic;

namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlSql
    {
        ISelector From(string tableName, bool withNoLock = false);

        IInsertable<TRes> InsertInto<TRes>(Func<TRes, (string tableName, IEnumerable<string> cols)> tableNameAndColsFunc) where TRes : struct;  
    }
}
