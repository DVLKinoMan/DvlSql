using System.Collections.Generic;
using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlFullSelectExpression : DvlSqlExpression
    {
        public DvlSqlFullSelectExpression() { }

        public DvlSqlFullSelectExpression(DvlSqlFromExpression fromExpression,
            List<DvlSqlJoinExpression> sqlJoinExpressions, DvlSqlWhereExpression sqlWhereExpression,
            DvlSqlGroupByExpression groupByExpression,
            DvlSqlSelectExpression selectExpression, DvlSqlOrderByExpression orderByExpression) => (
            this.SqlFromExpression, this.SqlJoinExpressions, this.SqlWhereExpression, this.SqlGroupByExpression,
            this.SqlSelectExpression, this.SqlOrderByExpression) = (fromExpression, sqlJoinExpressions ??  new List<DvlSqlJoinExpression>(),
            sqlWhereExpression, groupByExpression, selectExpression, orderByExpression);

        public DvlSqlFromExpression SqlFromExpression { get; set; }
        public List<DvlSqlJoinExpression> SqlJoinExpressions { get; private set; } = new List<DvlSqlJoinExpression>();
        public DvlSqlWhereExpression SqlWhereExpression { get; set; }
        public DvlSqlGroupByExpression SqlGroupByExpression { get; set; }
        public DvlSqlSelectExpression SqlSelectExpression { get; set; }
        public DvlSqlOrderByExpression SqlOrderByExpression { get; set; }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
