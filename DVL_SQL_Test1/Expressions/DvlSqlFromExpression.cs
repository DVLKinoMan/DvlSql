using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlFromExpression : DvlSqlExpression
    {
        public string TableName { get; }
        public bool WithNoLock { get; }

        public DvlSqlFromExpression(string tableName, bool withNoLock = false) =>
            (this.TableName, this.WithNoLock) = (tableName, withNoLock);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
