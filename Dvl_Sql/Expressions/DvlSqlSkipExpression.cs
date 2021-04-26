using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
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
    }
}
