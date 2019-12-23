using Dvl_Sql.Abstract;
using Dvl_Sql.Expressions;
using Dvl_Sql.Models;

namespace Dvl_Sql.Concrete
{
    internal class SqlUpdateSetable : IUpdateSetable
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
