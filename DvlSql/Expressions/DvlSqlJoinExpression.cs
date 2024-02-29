using DvlSql;

namespace DvlSql.Expressions
{
    public abstract class DvlSqlJoinExpression : DvlSqlExpression
    {
        public string TableName { get; init; }
        public new bool IsRoot { get; init; } = true;
        public DvlSqlComparisonExpression ComparisonExpression { get; set; }

        public DvlSqlJoinExpression(string tableName, DvlSqlComparisonExpression comp)
        {
            this.TableName = tableName;
            this.ComparisonExpression = comp;
        }

        // public DvlSqlJoinExpression WithRoot(bool isRoot)
        // {
        //     this.IsRoot = isRoot;
        //     return this;
        // }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }

    public class DvlSqlFullJoinExpression : DvlSqlJoinExpression
    {
        public DvlSqlFullJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression) : base(tableName, comparisonExpression)
        {
        }

        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }

    public class DvlSqlInnerJoinExpression : DvlSqlJoinExpression
    {
        public DvlSqlInnerJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression) : base(tableName, comparisonExpression)
        {
        }

        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }

    public class DvlSqlLeftJoinExpression : DvlSqlJoinExpression
    {
        public DvlSqlLeftJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression) : base(tableName, comparisonExpression)
        {
        }

        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }

    public class DvlSqlRightJoinExpression : DvlSqlJoinExpression
    {
        public DvlSqlRightJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression) : base(tableName, comparisonExpression)
        {
        }

        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
