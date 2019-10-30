using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;

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

    }
}
