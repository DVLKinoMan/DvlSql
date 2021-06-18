using DvlSql.Abstract;

namespace DvlSql.Expressions
{
    public abstract class DvlSqlJoinExpression : DvlSqlExpression
    {
        public string TableName { get; set; }
        public new bool IsRoot { get; set; } = true;
        public DvlSqlComparisonExpression ComparisonExpression { get; set; }

        // public DvlSqlJoinExpression WithRoot(bool isRoot)
        // {
        //     this.IsRoot = isRoot;
        //     return this;
        // }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }

    public class DvlSqlFullJoinExpression : DvlSqlJoinExpression
    {
        public DvlSqlFullJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression) =>
            (this.TableName, this.ComparisonExpression) = (tableName, comparisonExpression);

        //public override void Accept(ISqlExpressionVisitor visitor)
        //{
        //    visitor.Visit(this);
        //}
        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }

    public class DvlSqlInnerJoinExpression : DvlSqlJoinExpression
    {
        public DvlSqlInnerJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression) =>
            (this.TableName, this.ComparisonExpression) = (tableName, comparisonExpression);

        //public override void Accept(ISqlExpressionVisitor visitor)
        //{
        //    throw new System.NotImplementedException();
        //}
        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }

    public class DvlSqlLeftJoinExpression : DvlSqlJoinExpression
    {
        public DvlSqlLeftJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression) =>
            (this.TableName, this.ComparisonExpression) = (tableName, comparisonExpression);

        //public override void Accept(ISqlExpressionVisitor visitor)
        //{
        //    throw new System.NotImplementedException();
        //}
        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }

    public class DvlSqlRightJoinExpression : DvlSqlJoinExpression
    {
        public DvlSqlRightJoinExpression(string tableName, DvlSqlComparisonExpression comparisonExpression) =>
            (this.TableName, this.ComparisonExpression) = (tableName, comparisonExpression);

        //public override void Accept(ISqlExpressionVisitor visitor)
        //{
        //    throw new System.NotImplementedException();
        //}
        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
