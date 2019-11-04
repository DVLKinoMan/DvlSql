using System.Collections.Generic;
using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using DVL_SQL_Test1.Models;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlGrouper : IGrouper
    {
        private readonly SqlSelector _selector;

        public SqlGrouper(SqlSelector selector) => this._selector = selector;

        public ISelectable Having(DvlSqlBinaryExpression binaryExpression) => this._selector.Having(this, binaryExpression);

        public ISelectable Having(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params) => this._selector.Having(this, binaryExpression, @params);

        public IOrderer Select(params string[] parameterNames) => this._selector.Select(parameterNames);

        public IOrderer Select() => this._selector.Select();

        public IOrderer SelectTop(int count, params string[] parameterNames) =>
            this._selector.SelectTop(count, parameterNames);

    }
}
