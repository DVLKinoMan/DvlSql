using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DVL_SQL_Test1.Models;

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

        public IInsertable<TRes> InsertInto<TRes>(string tableName, params (string col, DvlSqlType sqlType)[] types)
            where TRes : ITuple
        {
            var insertExpression = new DvlSqlInsertIntoExpression<TRes>(tableName, types);

            return new SqlInsertable<TRes>(insertExpression, this._connectionString);
        }

        public IInsertable InsertInto(string tableName, IEnumerable<string> cols)
        {
            var insertExpression = new DvlSqlInsertIntoSelectExpression(tableName, cols.ToArray());

            return new SqlInsertable(insertExpression, this._connectionString);
        }

        public IDeletable DeleteFrom(string tableName)
        {
            var fromExpression = new DvlSqlFromExpression(tableName);

            return new SqlDeletable(fromExpression, this._connectionString);
        }

        public IUpdateSetable Update(string tableName)
        {
            var updateExpression = new DvlSqlUpdateExpression(tableName);

            return  new SqlUpdateSetable(this._connectionString, updateExpression);
        }
    }
}
