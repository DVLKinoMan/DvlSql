using System.Collections.Generic;
using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlAndExpression : DvlSqlBinaryExpression
    {
        public IEnumerable<DvlSqlExpression> InnerExpressions { get; }

        public DvlSqlAndExpression(params DvlSqlExpression[] innerExpressions) =>
            this.InnerExpressions = innerExpressions;

        public DvlSqlAndExpression(IEnumerable<DvlSqlExpression> innerExpressions) =>
            this.InnerExpressions = innerExpressions;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
