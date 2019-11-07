using System.Collections.Generic;
using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlAndExpression : DvlSqlBinaryExpression
    {
        public IEnumerable<DvlSqlBinaryExpression> InnerExpressions { get; }

        public DvlSqlAndExpression(params DvlSqlBinaryExpression[] binaryExpressions) =>
            this.InnerExpressions = binaryExpressions;

        public DvlSqlAndExpression(IEnumerable<DvlSqlBinaryExpression> binaryExpressions) =>
            this.InnerExpressions = binaryExpressions;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
