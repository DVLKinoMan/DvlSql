namespace DvlSql.Expressions;

public class DvlSqlLikeExpression : DvlSqlBinaryExpression
{
    public string Field { get; init; }
    public string Pattern { get; init; }

    public DvlSqlLikeExpression(string field, string pattern) => (this.Field, this.Pattern) = (field, pattern);

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => BinaryClone();

    public override DvlSqlBinaryExpression BinaryClone() => new DvlSqlLikeExpression(Field, Pattern).SetNot(Not);

    public override void NotOnThis()
    {
        this.Not = !this.Not;
    }
}
