using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlWhereExpression : DvlSqlExpressionWithParameters
    {
        public DvlSqlBinaryExpression InnerExpression { get; private set; }

        public DvlSqlWhereExpression(DvlSqlBinaryExpression expression) => (this.InnerExpression, this.IsRoot) = (expression, true);

        public DvlSqlWhereExpression WithRoot(bool isRoot)
        {
            this.IsRoot = isRoot;
            return this;
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public void Add(DvlSqlBinaryExpression binaryExp) => InnerExpression &= binaryExp;

        public static DvlSqlWhereExpression operator &(DvlSqlWhereExpression leftWhereExpression,
            DvlSqlBinaryExpression rightBinaryExpression) => new DvlSqlWhereExpression(leftWhereExpression.InnerExpression & rightBinaryExpression);
    }
}
