using System.Collections.Generic;
using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlOrExpression : DvlSqlExpression
    {
        public IEnumerable<DvlSqlExpression> InnerExpressions { get; }

        public DvlSqlOrExpression(params DvlSqlExpression[] innerExpressions) =>
            this.InnerExpressions = innerExpressions;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
