using DvlSql.Abstract;

namespace DvlSql.Expressions
{
    public class DvlSqlFromExpression : DvlSqlExpression
    {
        public string TableName { get; }
        public bool WithNoLock { get; }

        public DvlSqlFullSelectExpression FullSelect { get; }
        public string As { get; }

        public DvlSqlFromExpression(string tableName, bool withNoLock = false) =>
            (this.TableName, this.WithNoLock) = (tableName, withNoLock);

        public DvlSqlFromExpression(DvlSqlFullSelectExpression fullSelect, string @as) =>
            (this.FullSelect, this.As) = (fullSelect, @as);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
