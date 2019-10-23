using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlComparisonExpression : DvlSqlExpression
    {
        public DvlSqlExpression LeftExpression { get; }
        public SqlComparisonOperator ComparisonOperator { get; }
        public DvlSqlExpression RightExpression { get; }

        public DvlSqlComparisonExpression(DvlSqlExpression leftExpression, SqlComparisonOperator comparisonOperator,
            DvlSqlExpression rightExpression) => (this.LeftExpression, this.ComparisonOperator, this.RightExpression) =
            (leftExpression, comparisonOperator, rightExpression);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
