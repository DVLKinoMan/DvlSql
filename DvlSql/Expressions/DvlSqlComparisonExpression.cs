namespace DvlSql.Expressions;

public abstract class DvlSqlComparisonExpression : DvlSqlBinaryExpression
{
    public SqlComparisonOperator ComparisonOperator { get; init; }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
}

public class DvlSqlComparisonExpression<T> : DvlSqlComparisonExpression
{
    public DvlSqlComparableExpression<T> LeftExpression { get; init; }
    public DvlSqlComparableExpression<T> RightExpression { get; init; }

    public DvlSqlComparisonExpression(DvlSqlComparableExpression<T> leftExpression, SqlComparisonOperator comparisonOperator,
        DvlSqlComparableExpression<T> rightExpression) => (this.LeftExpression, this.ComparisonOperator, this.RightExpression) =
        (leftExpression, comparisonOperator, rightExpression);

    public override DvlSqlExpression Clone() => BinaryClone();

    public override DvlSqlBinaryExpression BinaryClone() =>
        new DvlSqlComparisonExpression<T>(LeftExpression.ComparableClone(), ComparisonOperator, RightExpression.ComparableClone())
            .SetNot(Not);

    public override void NotOnThis()
    {
        this.Not = !this.Not;
    }
}
