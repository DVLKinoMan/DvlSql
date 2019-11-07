using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlNotExpression : DvlSqlBinaryExpression
    {
        public DvlSqlBinaryExpression BinaryExpression;

        public DvlSqlNotExpression(DvlSqlBinaryExpression binaryExpression) =>
            this.BinaryExpression = binaryExpression;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
