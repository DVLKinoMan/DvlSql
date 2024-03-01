namespace DvlSql.Expressions
{
    public class DvlSqlSkipExpression(int offsetRows, int? fetchNextRows = null) : DvlSqlExpression
    {
        public int OffsetRows { get; init; } = offsetRows;
        public int? FetchNextRows { get; set; } = fetchNextRows;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => SkipClone();

        public DvlSqlSkipExpression SkipClone() => new(OffsetRows, FetchNextRows);
    }
}
