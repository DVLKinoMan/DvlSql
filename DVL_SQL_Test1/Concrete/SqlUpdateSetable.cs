using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlUpdateSetable : IUpdateSetable
    {
        private readonly string _connString;
        private SqlUpdateable _sqlUpdateable;
        private readonly DvlSqlUpdateExpression _updateExpression;

        public SqlUpdateSetable(string connString, DvlSqlUpdateExpression updateExpression) =>
            (this._connString, this._updateExpression) = (connString, updateExpression);

        public IUpdateable Set<TVal>((string, TVal) value)
        {
            this._updateExpression.Add(value);

            if (this._sqlUpdateable != null)
                return this._sqlUpdateable;

            return this._sqlUpdateable = new SqlUpdateable(this._connString, this._updateExpression, this);
        }
    }
}
