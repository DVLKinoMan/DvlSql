using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlDeleteExpression : DvlSqlExpression
    {
        public DvlSqlFromExpression FromExpression { get; set; }
        public DvlSqlWhereExpression WhereExpression { get; set; }

        public DvlSqlDeleteExpression(DvlSqlFromExpression fromExpression) =>
            this.FromExpression = fromExpression;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
