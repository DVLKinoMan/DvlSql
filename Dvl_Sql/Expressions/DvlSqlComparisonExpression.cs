using Dvl_Sql.Abstract;
using Dvl_Sql.Helpers;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlComparisonExpression : DvlSqlBinaryExpression
    {
        public DvlSqlConstantExpression LeftExpression { get; }
        public SqlComparisonOperator ComparisonOperator { get; }
        public DvlSqlConstantExpression RightExpression { get; }

        public DvlSqlComparisonExpression(DvlSqlConstantExpression leftExpression, SqlComparisonOperator comparisonOperator,
            DvlSqlConstantExpression rightExpression) => (this.LeftExpression, this.ComparisonOperator, this.RightExpression) =
            (leftExpression, comparisonOperator, rightExpression);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => BinaryClone();

        public override DvlSqlBinaryExpression BinaryClone() =>
            new DvlSqlComparisonExpression(LeftExpression.ConstantClone(), ComparisonOperator, RightExpression.ConstantClone())
                .SetNot(Not);

        public override void NotOnThis()
        {
            this.Not = !this.Not;
        }
    }
}
