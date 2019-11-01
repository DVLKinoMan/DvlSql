using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlDeleteExpression : DvlSqlExpression
    {
        public DvlSqlFromExpression FromExpression { get; set; }
        public DvlSqlWhereExpression WhereExpression { get; set; }

        public DvlSqlDeleteExpression(DvlSqlFromExpression fromExpression) =>
            this.FromExpression = fromExpression;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
