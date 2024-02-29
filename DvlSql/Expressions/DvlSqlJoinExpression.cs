namespace DvlSql.Expressions
{
    public abstract class DvlSqlJoinExpression (string tableName, DvlSqlComparisonExpression comp) : DvlSqlExpression
    {
        public string TableName { get; init; } = tableName;
        public new bool IsRoot { get; init; } = true;
        public DvlSqlComparisonExpression ComparisonExpression { get; set; } = comp;

        // public DvlSqlJoinExpression WithRoot(bool isRoot)
        // {
        //     this.IsRoot = isRoot;
        //     return this;
        // }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }

    public class DvlSqlFullJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression) : DvlSqlJoinExpression(tableName, comparisonExpression)
    {
        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }

    public class DvlSqlInnerJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression) : DvlSqlJoinExpression(tableName, comparisonExpression)
    {
        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }

    public class DvlSqlLeftJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression) : DvlSqlJoinExpression(tableName, comparisonExpression)
    {
        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }

    public class DvlSqlRightJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression) : DvlSqlJoinExpression(tableName, comparisonExpression)
    {
        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
