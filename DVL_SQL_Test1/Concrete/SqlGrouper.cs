using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlGrouper : IGrouper
    {
        private readonly SqlSelector _selector;

        public SqlGrouper(SqlSelector selector) => this._selector = selector;

        public ISelectable Having(DvlSqlBinaryExpression binaryExpression) => this._selector.Having(this, binaryExpression);

        public IOrderer Select(params string[] parameterNames) => this._selector.Select(parameterNames);

        public IOrderer Select() => this._selector.Select();

        public IOrderer SelectTop(int count, params string[] parameterNames) =>
            this._selector.SelectTop(count, parameterNames);

    }
}
