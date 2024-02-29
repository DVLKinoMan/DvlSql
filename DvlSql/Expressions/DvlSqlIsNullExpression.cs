namespace DvlSql.Expressions
{
    public class DvlSqlIsNullExpression : DvlSqlBinaryExpression
    {
        public DvlSqlExpression Expression;

        public DvlSqlIsNullExpression(DvlSqlExpression expression) => this.Expression = expression;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => BinaryClone();

        public override DvlSqlBinaryExpression BinaryClone() => 
            new DvlSqlIsNullExpression(Expression.Clone()).SetNot(Not);

        public override void NotOnThis()
        {
            this.Not = !this.Not;
        }
    }
}
