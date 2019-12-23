using System.Collections.Generic;
using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlInExpression : DvlSqlBinaryExpression
    {
        public string ParameterName { get; }
        public IEnumerable<DvlSqlExpression> InnerExpressions { get; }

        public DvlSqlInExpression(string parameterName, params DvlSqlExpression[] innerExpressions) =>
            (this.ParameterName, this.InnerExpressions) = (parameterName, innerExpressions);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
