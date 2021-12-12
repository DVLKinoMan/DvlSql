using System.Collections.Generic;
using System.Linq;

namespace DvlSql.Expressions
{
    public class DvlSqlGroupByExpression : DvlSqlExpressionWithParameters
    {
        public List<string> ParameterNames;
        public DvlSqlBinaryExpression? BinaryExpression { get; set; }

        public DvlSqlGroupByExpression(IEnumerable<string> parameterNames) => (this.ParameterNames, this.IsRoot) = (parameterNames.ToList(), true);

        // public DvlSqlGroupByExpression WithRoot(bool isRoot)
        // {
        //     this.IsRoot = isRoot;
        //     return this;
        // }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => GroupByClone();

        public DvlSqlGroupByExpression GroupByClone() => new DvlSqlGroupByExpression(this.ParameterNames.ToArray());
    }
}
