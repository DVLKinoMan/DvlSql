using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlWhereExpression : DvlSqlExpression
    {
        public DvlSqlExpression InnerExpression { get; }

        public DvlSqlWhereExpression(DvlSqlBinaryExpression expression) => (this.InnerExpression, this.IsRoot) = (expression, true);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
