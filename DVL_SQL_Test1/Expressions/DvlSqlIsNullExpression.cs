using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlIsNullExpression : DvlSqlBinaryExpression
    {
        public DvlSqlExpression Expression;

        public DvlSqlIsNullExpression(DvlSqlExpression expression) => this.Expression = expression;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
