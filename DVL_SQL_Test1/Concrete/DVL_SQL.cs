using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Concrete
{
    public class DVL_SQL : IDvlFrom
    {
        private string _connectionString;

        public DVL_SQL(string connectionString) => this._connectionString = connectionString;

        public IDvlSelectable From(string tableName)
        {
            var fromExpression = new DvlSqlFromExpression(tableName);
            return new DvlSqlSelectable(fromExpression);
        }

    }
}
