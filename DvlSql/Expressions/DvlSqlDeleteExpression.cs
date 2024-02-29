using System.Collections.Generic;

namespace DvlSql.Expressions
{
    public class DvlSqlDeleteExpression : DvlSqlExpression
    {
        public DvlSqlFromWithTableExpression FromExpression { get; init; }
        public DvlSqlWhereExpression? WhereExpression { get; set; }
        public DvlSqlOutputExpression? OutputExpression { get; set; }
        public List<DvlSqlJoinExpression>? Join { get; private set; } = new List<DvlSqlJoinExpression>();

        public DvlSqlDeleteExpression(DvlSqlFromWithTableExpression fromExpression) =>
            this.FromExpression = fromExpression;

        public void AddJoin(DvlSqlJoinExpression exp)
        {
            Join ??= [];
            Join.Add(exp);
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
