using System.Collections.Generic;
using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
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
