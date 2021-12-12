using System.Collections.Generic;
using System.Linq;

namespace DvlSql.Expressions
{
    public class DvlSqlFullSelectExpression : DvlSqlFromExpression
    {
        public DvlSqlFullSelectExpression(DvlSqlFromExpression @from, DvlSqlSelectExpression @select)
        {
            From = from;
            Select = select;
        }

        public DvlSqlFullSelectExpression(DvlSqlFromExpression @from,
            List<DvlSqlJoinExpression> joins, DvlSqlWhereExpression @where,
            DvlSqlGroupByExpression groupBy,
            DvlSqlSelectExpression @select, DvlSqlOrderByExpression orderBy, DvlSqlSkipExpression skip,
            DvlSqlAsExpression @as) =>
        (
            this.From, this.Join, this.Where, this.GroupBy,
            this.Select, this.OrderBy, this.Skip, this.As) = (@from, joins ?? new List<DvlSqlJoinExpression>(),
            @where, groupBy, @select, orderBy, skip, @as);

        public DvlSqlFromExpression From { get; set; }
        public List<DvlSqlJoinExpression>? Join { get; private set; } = new List<DvlSqlJoinExpression>();
        public DvlSqlWhereExpression? Where { get; set; }
        public DvlSqlGroupByExpression? GroupBy { get; set; }
        public DvlSqlSelectExpression Select { get; set; }
        public DvlSqlOrderByExpression? OrderBy { get; set; }
        public DvlSqlSkipExpression? Skip { get; set; }

        public override void Accept(ISqlExpressionVisitor visitor)
        {
            if (As is null)
                visitor.Visit(this);
            else visitor.Visit(this as DvlSqlFromExpression);
        }

        public override DvlSqlExpression Clone() => FullSelectClone();

        public void AddJoin(DvlSqlJoinExpression exp)
        {
            Join ??= new List<DvlSqlJoinExpression>();
            Join.Add(exp);
        }

        public DvlSqlFullSelectExpression FullSelectClone()
        {
            var clone = new DvlSqlFullSelectExpression(From, Select.SelectClone());
            if (Join != null)
                clone.Join = Join.ToList();
            if (Where != null)
                clone.Where = Where.WhereClone();
            if (GroupBy != null)
                clone.GroupBy = GroupBy.GroupByClone();
            if (OrderBy != null)
                clone.OrderBy = OrderBy.OrderByClone();
            if (Skip != null)
                clone.Skip = Skip.SkipClone();

            return clone;
        }
    }
}
