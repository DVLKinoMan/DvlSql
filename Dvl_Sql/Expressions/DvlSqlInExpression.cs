using DvlSql;
using System.Collections.Generic;
using System.Linq;

namespace DvlSql.Expressions
{
    public class DvlSqlInExpression : DvlSqlBinaryExpression
    {
        public string ParameterName { get; }
        public IEnumerable<DvlSqlExpression> InnerExpressions { get; }

        public DvlSqlInExpression(string parameterName, params DvlSqlExpression[] innerExpressions) =>
            (this.ParameterName, this.InnerExpressions) = (parameterName, innerExpressions);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => BinaryClone();

        public override DvlSqlBinaryExpression BinaryClone() =>
             new DvlSqlInExpression(ParameterName, InnerExpressions.Select(inner => inner.Clone()).ToArray())
                 .SetNot(Not);

        public override void NotOnThis()
        {
            this.Not = !this.Not;
        }
    }
}
