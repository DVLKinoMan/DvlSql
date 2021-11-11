using System.Collections.Generic;
using System.Linq;


namespace DvlSql.Expressions
{
    public class DvlSqlAndExpression : DvlSqlBinaryExpression
    {
        public IEnumerable<DvlSqlBinaryExpression> InnerExpressions { get; }

        public DvlSqlAndExpression(params DvlSqlBinaryExpression[] binaryExpressions) =>
            this.InnerExpressions = binaryExpressions;

        public DvlSqlAndExpression(IEnumerable<DvlSqlBinaryExpression> binaryExpressions) =>
            this.InnerExpressions = binaryExpressions;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => BinaryClone();

        public override DvlSqlBinaryExpression BinaryClone() =>
            new DvlSqlAndExpression(InnerExpressions.Select(inner => inner.BinaryClone()))
                .SetNot(Not);

        public override void NotOnThis()
        {
            this.Not = !this.Not;
            foreach (var inner in this.InnerExpressions)
            {
                var something = !inner;
            }
        }
    }
}
