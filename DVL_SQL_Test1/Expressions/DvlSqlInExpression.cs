using DVL_SQL_Test1.Abstract;
using System.Collections.Generic;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlInExpression : DvlSqlExpression
    {
        public string ParameterName { get; }
        public IEnumerable<DvlSqlExpression> InnerExpressions { get; }

        public DvlSqlInExpression(string parameterName, params DvlSqlExpression[] innerExpressions) =>
            (this.ParameterName, this.InnerExpressions) = (parameterName, innerExpressions);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
