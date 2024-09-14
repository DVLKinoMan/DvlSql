using System.Collections.Generic;
using System.Linq;

namespace DvlSql.Expressions;

public class DvlSqlOrExpression(params DvlSqlBinaryExpression[] binaryExpressions) : DvlSqlBinaryExpression
{
    public IEnumerable<DvlSqlBinaryExpression> InnerExpressions { get; init; } = binaryExpressions;

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => BinaryClone();

    public override DvlSqlBinaryExpression BinaryClone() =>
        new DvlSqlOrExpression(InnerExpressions.Select(inner => inner.BinaryClone()).ToArray())
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
