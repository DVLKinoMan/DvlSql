using System.Collections.Generic;
using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlFullSelectExpression : DvlSqlExpression
    {
        public DvlSqlFullSelectExpression() { }

        public DvlSqlFullSelectExpression(DvlSqlFromExpression @from,
            List<DvlSqlJoinExpression> joins, DvlSqlWhereExpression @where,
            DvlSqlGroupByExpression groupBy,
            DvlSqlSelectExpression @select, DvlSqlOrderByExpression orderBy) => (
            this.From, this.Join, this.Where, this.GroupBy,
            this.Select, this.OrderBy) = (@from, joins ??  new List<DvlSqlJoinExpression>(),
            @where, groupBy, @select, orderBy);

        public DvlSqlFromExpression From { get; set; }
        public List<DvlSqlJoinExpression> Join { get; private set; } = new List<DvlSqlJoinExpression>();
        public DvlSqlWhereExpression Where { get; set; }
        public DvlSqlGroupByExpression GroupBy { get; set; }
        public DvlSqlSelectExpression Select { get; set; }
        public DvlSqlOrderByExpression OrderBy { get; set; }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
