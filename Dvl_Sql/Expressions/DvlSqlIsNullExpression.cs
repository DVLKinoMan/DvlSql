using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlIsNullExpression : DvlSqlBinaryExpression
    {
        public DvlSqlExpression Expression;

        public DvlSqlIsNullExpression(DvlSqlExpression expression) => this.Expression = expression;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override void NotOnThis()
        {
            this.Not = !this.Not;
        }
    }
}
