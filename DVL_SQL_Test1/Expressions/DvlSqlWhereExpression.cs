using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlWhereExpression : DvlSqlExpression
    {
        public DvlSqlExpression InnerExpression { get; }

        public DvlSqlWhereExpression(DvlSqlExpression expression) => this.InnerExpression = expression;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
