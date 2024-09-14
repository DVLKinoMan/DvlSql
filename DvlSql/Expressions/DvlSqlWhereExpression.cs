namespace DvlSql.Expressions;

public class DvlSqlWhereExpression : DvlSqlExpressionWithParameters
{
    public DvlSqlBinaryExpression InnerExpression { get; private set; }

    public DvlSqlWhereExpression(DvlSqlBinaryExpression expression) => (this.InnerExpression, this.IsRoot) = (expression, true);

    //public DvlSqlWhereExpression()
    //{
    //}

    public DvlSqlWhereExpression WithRoot(bool isRoot)
    {
        this.IsRoot = isRoot;
        return this;
    }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => WhereClone();
    
    public DvlSqlWhereExpression WhereClone() => new(InnerExpression.BinaryClone());

    public void Add(DvlSqlBinaryExpression binaryExp) => InnerExpression = InnerExpression is null ? binaryExp : InnerExpression & binaryExp;

    public static DvlSqlWhereExpression operator &(DvlSqlWhereExpression leftWhereExpression,
        DvlSqlBinaryExpression rightBinaryExpression) => new(leftWhereExpression.InnerExpression & rightBinaryExpression);

    public static DvlSqlWhereExpression operator !(DvlSqlWhereExpression where) => 
        new(!where.InnerExpression);
}
