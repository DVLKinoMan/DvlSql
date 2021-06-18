using DvlSql.Abstract;

namespace DvlSql.Expressions
{
    public class DvlSqlSkipExpression : DvlSqlExpression
    {
        public int OffsetRows { get; set; }
        public int? FetchNextRows { get; set; }

        public DvlSqlSkipExpression(int offsetRows, int? fetchNextRows = null)
        {
            OffsetRows = offsetRows;
            FetchNextRows = fetchNextRows;
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => SkipClone();

        public DvlSqlSkipExpression SkipClone() => new DvlSqlSkipExpression(OffsetRows, FetchNextRows);
    }
}
