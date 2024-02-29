namespace DvlSql.Expressions
{
    public class DvlSqlSkipExpression : DvlSqlExpression
    {
        public int OffsetRows { get; init; }
        public int? FetchNextRows { get; init; }

        public DvlSqlSkipExpression(int offsetRows, int? fetchNextRows = null)
        {
            OffsetRows = offsetRows;
            FetchNextRows = fetchNextRows;
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => SkipClone();

        public DvlSqlSkipExpression SkipClone() => new(OffsetRows, FetchNextRows);
    }
}
