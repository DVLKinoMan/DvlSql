using System.Collections.Generic;
using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlFullSelectExpression : DvlSqlExpression
    {
        public DvlSqlFullSelectExpression() { }

        public DvlSqlFullSelectExpression(DvlSqlFromExpression fromExpression,
            List<DvlSqlJoinExpression> joinExpressions, DvlSqlWhereExpression whereExpression,
            DvlSqlGroupByExpression groupByExpression,
            DvlSqlSelectExpression selectExpression, DvlSqlOrderByExpression orderByExpression) => (
            this.FromExpression, this.JoinExpressions, this.WhereExpression, this.GroupByExpression,
            this.SelectExpression, this.OrderByExpression) = (fromExpression, joinExpressions ??  new List<DvlSqlJoinExpression>(),
            whereExpression, groupByExpression, selectExpression, orderByExpression);

        public DvlSqlFromExpression FromExpression { get; set; }
        public List<DvlSqlJoinExpression> JoinExpressions { get; private set; } = new List<DvlSqlJoinExpression>();
        public DvlSqlWhereExpression WhereExpression { get; set; }
        public DvlSqlGroupByExpression GroupByExpression { get; set; }
        public DvlSqlSelectExpression SelectExpression { get; set; }
        public DvlSqlOrderByExpression OrderByExpression { get; set; }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
