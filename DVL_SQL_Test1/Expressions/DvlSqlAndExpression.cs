using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlAndExpression : DvlSqlExpression
    {
        public DvlSqlExpression LeftExpression { get; }
        public DvlSqlExpression RightExpression { get; }

        public DvlSqlAndExpression(DvlSqlExpression left, DvlSqlExpression right) =>
            (this.LeftExpression, this.RightExpression) = (left, right);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
