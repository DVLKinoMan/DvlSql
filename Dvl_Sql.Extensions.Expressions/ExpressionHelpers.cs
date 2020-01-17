﻿using System.Collections.Generic;
using Dvl_Sql.Expressions;

namespace Dvl_Sql.Extensions.Expressions
{
    public static partial class ExpressionHelpers
    {
        public static DvlSqlWhereExpression WhereExp(DvlSqlBinaryExpression innerExpression, bool isRoot = false) =>
            new DvlSqlWhereExpression(innerExpression).WithRoot(isRoot);

        public static DvlSqlAndExpression AndExp(params DvlSqlBinaryExpression[] innerExpressions) =>
            new DvlSqlAndExpression(innerExpressions);

        public static DvlSqlAndExpression AndExp(IEnumerable<DvlSqlBinaryExpression> innerExpressions) =>
            new DvlSqlAndExpression(innerExpressions);

        public static DvlSqlComparisonExpression ComparisonExp(DvlSqlConstantExpression leftExp, SqlComparisonOperator op,
            DvlSqlConstantExpression rightExp)
            => new DvlSqlComparisonExpression(leftExp, op, rightExp);

        public static DvlSqlConstantExpression<TValue> ConstantExp<TValue>(TValue value) =>
            new DvlSqlConstantExpression<TValue>(value);

        public static DvlSqlOrExpression OrExp(params DvlSqlBinaryExpression[] innerExpressions) =>
            new DvlSqlOrExpression(innerExpressions);

        public static DvlSqlInExpression InExp(string parameterName, params DvlSqlExpression[] innerExpressions) =>
            new DvlSqlInExpression(parameterName, innerExpressions);

        public static DvlSqlSelectExpression SelectExp(DvlSqlFromExpression fromExp, int? topNum = null,
            bool isRoot = false) =>
            new DvlSqlSelectExpression(fromExp, topNum).WithRoot(isRoot);

        public static DvlSqlFromExpression FromExp(string tableName, bool withNoLock = false) =>
            new DvlSqlFromExpression(tableName, withNoLock);

        public static DvlSqlSelectExpression SelectExp(DvlSqlFromExpression fromExp, params string[] paramNames) =>
            new DvlSqlSelectExpression(fromExp, paramNames).WithRoot(false);

        public static DvlSqlSelectExpression SelectTopExp(DvlSqlFromExpression fromExp, int topNum,
            params string[] paramNames) =>
            new DvlSqlSelectExpression(fromExp, paramNames, topNum).WithRoot(false);

        public static DvlSqlLikeExpression LikeExp(string field, string pattern) =>
            new DvlSqlLikeExpression(field, pattern);

        public static DvlSqlNotExpression NotExp(DvlSqlBinaryExpression binaryExpression) =>
            new DvlSqlNotExpression(binaryExpression);

        public static DvlSqlNotExpression NotInExp(string parameterName, params DvlSqlExpression[] innerExpressions) =>
            NotExp(InExp(parameterName, innerExpressions));

        public static DvlSqlNotExpression NotLikeExp(string field, string pattern) => NotExp(LikeExp(field, pattern));

        public static DvlSqlIsNullExpression IsNullExp(DvlSqlExpression expression) =>
            new DvlSqlIsNullExpression(expression);

        public static DvlSqlNotExpression IsNotNullExp(DvlSqlExpression expression) => NotExp(IsNullExp(expression));

        public static DvlSqlFullSelectExpression FullSelectExp(
            DvlSqlSelectExpression selectExpression,
            List<DvlSqlJoinExpression> sqlJoinExpressions = null, DvlSqlWhereExpression sqlWhereExpression = null,
            DvlSqlGroupByExpression groupByExpression = null, DvlSqlOrderByExpression orderByExpression = null) =>
            new DvlSqlFullSelectExpression(selectExpression.FromExpression, sqlJoinExpressions, sqlWhereExpression, groupByExpression,
                selectExpression, orderByExpression);

        public static DvlSqlOrderByExpression OrderByExp(params (string column, Ordering ordering)[] @params) =>
            new DvlSqlOrderByExpression(@params);
    }
}
