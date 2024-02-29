using System.Collections.Generic;

namespace DvlSql.Expressions
{
    public class DvlSqlDeleteExpression(DvlSqlFromWithTableExpression fromExpression) : DvlSqlExpression
    {
        public DvlSqlFromWithTableExpression FromExpression { get; init; } = fromExpression;
        public DvlSqlWhereExpression? WhereExpression { get; set; }
        public DvlSqlOutputExpression? OutputExpression { get; set; }
        public List<DvlSqlJoinExpression>? Join { get; private set; } = [];

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
