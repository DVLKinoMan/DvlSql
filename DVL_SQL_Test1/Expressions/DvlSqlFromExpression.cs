using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlFromExpression : DvlSqlExpression
    {
        public string TableName { get; }
        public DvlSqlFromExpression(string tableName) => this.TableName = tableName;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
