using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

        public IInsertable<TRes> InsertInto<TRes>(string tableName, IEnumerable<string> cols) where TRes : ITuple
        {
            var insertExpression = new DvlSqlInsertIntoExpression<TRes>(tableName, cols);

            return new SqlInsertable<TRes>(insertExpression, this._connectionString);
        }

        public IInsertable InsertInto(string tableName, IEnumerable<string> cols)
        {
            var insertExpression = new DvlSqlInsertIntoSelectExpression(tableName, cols);

            return new SqlInsertable(insertExpression, this._connectionString);
        }

        public IDeletable DeleteFrom(string tableName)
        {
            var fromExpression = new DvlSqlFromExpression(tableName);

            return new SqlDeletable(fromExpression, this._connectionString);
        }
    }
}
