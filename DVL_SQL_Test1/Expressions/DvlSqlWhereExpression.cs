using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlWhereExpression : DvlSqlExpression
    {
        public DvlSqlBinaryExpression InnerExpression { get; }

        public DvlSqlWhereExpression(DvlSqlBinaryExpression expression) => (this.InnerExpression, this.IsRoot) = (expression, true);

        public DvlSqlWhereExpression WithRoot(bool isRoot)
        {
            this.IsRoot = isRoot;
            return this;
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
