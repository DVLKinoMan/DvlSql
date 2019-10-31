using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using System;
using System.Collections.Generic;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSql : IDvlSql
    {
        private readonly string _connectionString;

        public DvlSql(string connectionString) => this._connectionString = connectionString;

        public ISelector From(string tableName, bool withNoLock = false)
        {
            var fromExpression = new DvlSqlFromExpression(tableName, withNoLock);

            return new SqlSelector(fromExpression, this._connectionString);
        }

        public IInsertable<TRes> InsertInto<TRes>(Func<TRes, (string tableName, IEnumerable<string> cols)> tableNameAndColsFunc) where TRes : struct
        {
            return new SqlInsertable<TRes>();
        }
    }
}
