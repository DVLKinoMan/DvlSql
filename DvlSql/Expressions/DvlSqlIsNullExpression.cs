namespace DvlSql.Expressions
{
    public class DvlSqlIsNullExpression(DvlSqlExpression expression) : DvlSqlBinaryExpression
    {
        public DvlSqlExpression Expression = expression;

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
