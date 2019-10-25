using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSql : IDvlFrom
    {
        private readonly string _connectionString;

        public DvlSql(string connectionString) => this._connectionString = connectionString;

        public IDvlSelectable From(string tableName)
        {
            var fromExpression = new DvlSqlFromExpression(tableName);

            return new DvlSqlSelectable(fromExpression, this._connectionString);
        }

    }
}
