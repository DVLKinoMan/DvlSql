using DVL_SQL_Test1.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlGroupByExpression : DvlSqlExpressionWithParameters
    {
        public List<string> ParameterNames;
        public DvlSqlBinaryExpression BinaryExpression { get; set; }

        public DvlSqlGroupByExpression(IEnumerable<string> parameterNames) => (this.ParameterNames, this.IsRoot) = (parameterNames.ToList(), true);

        public DvlSqlGroupByExpression WithRoot(bool isRoot)
        {
            this.IsRoot = isRoot;
            return this;
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
