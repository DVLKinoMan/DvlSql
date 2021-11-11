using DvlSql.Abstract;

namespace DvlSql.Models
{

    public abstract class DvlSqlFromExpression : DvlSqlExpression
    {
        public DvlSqlAsExpression As { get; set; }
    }

    public class DvlSqlFromWithTableExpression : DvlSqlFromExpression
    {
        public string TableName { get; }
        public bool WithNoLock { get; }

        public DvlSqlFromWithTableExpression(string tableName, bool withNoLock = false) =>
            (this.TableName, this.WithNoLock) = (tableName, withNoLock);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
