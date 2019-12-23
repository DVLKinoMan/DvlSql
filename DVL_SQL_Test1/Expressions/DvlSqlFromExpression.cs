using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
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
