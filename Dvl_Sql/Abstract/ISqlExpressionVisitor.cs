using System.Runtime.CompilerServices;
using DvlSql.Expressions;

namespace DvlSql.Abstract
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
        //void Visit(DvlSqlNotExpression expression);
        void Visit(DvlSqlLikeExpression expression);
        void Visit(DvlSqlIsNullExpression expression);
        void Visit<TParam>(DvlSqlInsertIntoExpression<TParam> expression) where TParam : ITuple;
        void Visit(DvlSqlInsertIntoSelectExpression expression);
        void Visit(DvlSqlFullSelectExpression expression);
        void Visit(DvlSqlDeleteExpression expression);
        void Visit(DvlSqlUpdateExpression expression);
        void Visit(DvlSqlUnionExpression expression);
        void Visit(DvlSqlExistsExpression expression);
        void Visit(DvlSqlSkipExpression expression);
        void Visit(DvlSqlTableDeclarationExpression expression);
        void Visit(DvlSqlOutputExpression expression);
        void Visit<T>(DvlSqlValuesExpression<T> expression) where T : ITuple;
        void Visit(DvlSqlAsExpression expression);
        void Visit(DvlSqlBinaryEmptyExpression expression);
    }
}
