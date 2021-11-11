using DvlSql.Abstract;

namespace DvlSql.Models
{
    public abstract class DvlSqlBinaryExpression : DvlSqlExpression
    {
        public bool Not { get; set; } = false;

        public static DvlSqlAndExpression operator &(DvlSqlBinaryExpression leftBinaryExpression,
            DvlSqlBinaryExpression rightBinaryExpression) =>
            new DvlSqlAndExpression(leftBinaryExpression, rightBinaryExpression);

        public static DvlSqlOrExpression operator |(DvlSqlBinaryExpression leftBinaryExpression,
            DvlSqlBinaryExpression rightBinaryExpression) =>
            new DvlSqlOrExpression(leftBinaryExpression, rightBinaryExpression);

        public static DvlSqlBinaryExpression operator !(DvlSqlBinaryExpression binaryExpression)
        {
            binaryExpression.NotOnThis();
            return binaryExpression;
        }

        public abstract DvlSqlBinaryExpression BinaryClone();

        public abstract void NotOnThis();
    }

    public class DvlSqlBinaryEmptyExpression : DvlSqlBinaryExpression
    {
        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone()
        {
            return this;
        }

        public override DvlSqlBinaryExpression BinaryClone()
        {
            return this;
        }

        public override void NotOnThis()
        {
            this.Not = !this.Not;
        }
    }
}
