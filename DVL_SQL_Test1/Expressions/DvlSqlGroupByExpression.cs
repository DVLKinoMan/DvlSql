using DVL_SQL_Test1.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlGroupByExpression : DvlSqlExpression
    {
        public List<string> ParameterNames;

        public DvlSqlGroupByExpression(IEnumerable<string> parameterNames) => (this.ParameterNames, this.IsRoot) = (parameterNames.ToList(), true);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
