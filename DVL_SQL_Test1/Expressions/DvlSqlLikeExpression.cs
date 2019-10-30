using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlLikeExpression : DvlSqlBinaryExpression
    {
        public string Field { get; set; }
        public string Pattern { get; set; }

        public DvlSqlLikeExpression(string field, string pattern) => (this.Field, this.Pattern) = (field, pattern);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
