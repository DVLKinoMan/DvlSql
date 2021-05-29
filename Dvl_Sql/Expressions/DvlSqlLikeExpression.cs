using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlLikeExpression : DvlSqlBinaryExpression
    {
        public string Field { get; set; }
        public string Pattern { get; set; }

        public DvlSqlLikeExpression(string field, string pattern) => (this.Field, this.Pattern) = (field, pattern);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => BinaryClone();

        public override DvlSqlBinaryExpression BinaryClone() => new DvlSqlLikeExpression(Field, Pattern);

        public override void NotOnThis()
        {
            this.Not = !this.Not;
        }
    }
}
