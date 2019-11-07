using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using DVL_SQL_Test1.Models;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlUpdateSetable : IUpdateSetable
    {
        private readonly IDvlSqlConnection _dvlSqlConnection;
        private SqlUpdateable _sqlUpdateable;
        private readonly DvlSqlUpdateExpression _updateExpression;

        public SqlUpdateSetable(IDvlSqlConnection dvlSqlConnection, DvlSqlUpdateExpression updateExpression) =>
            (this._dvlSqlConnection, this._updateExpression) = (dvlSqlConnection, updateExpression);

        public IUpdateable Set<TVal>((string, DvlSqlType<TVal>) value)
        {
            this._updateExpression.Add(value);

            if (this._sqlUpdateable != null)
                return this._sqlUpdateable;

            return this._sqlUpdateable = new SqlUpdateable(this._dvlSqlConnection, this._updateExpression, this);
        }
    }
}
