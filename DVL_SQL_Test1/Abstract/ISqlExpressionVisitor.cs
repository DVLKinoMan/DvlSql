using System.Runtime.CompilerServices;
using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Abstract
{
    public interface ISqlExpressionVisitor
    {
        void Visit(DvlSqlInExpression expression);
        void Visit(DvlSqlOrExpression expression);
        void Visit(DvlSqlAndExpression expression);
        void Visit(DvlSqlSelectExpression expression);
        void Visit(DvlSqlWhereExpression expression);
        void Visit(DvlSqlComparisonExpression expression);
        void Visit<TValue>(DvlSqlConstantExpression<TValue> expression);
        void Visit(DvlSqlFromExpression expression);
        void Visit(DvlSqlJoinExpression expression);
        void Visit(DvlSqlOrderByExpression expression);
        void Visit(DvlSqlGroupByExpression expression);
        void Visit(DvlSqlNotExpression expression);
        void Visit(DvlSqlLikeExpression expression);
        void Visit(DvlSqlIsNullExpression expression);
        void Visit<TParam>(DvlSqlInsertIntoExpression<TParam> expression) where TParam : ITuple;
        void Visit(DvlSqlInsertIntoSelectExpression expression);
        void Visit(DvlSqlFullSelectExpression expression);
    }
}
