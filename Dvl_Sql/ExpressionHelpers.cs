using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DvlSql.Expressions;
using DvlSql.Models;

namespace DvlSql
{
    public static class ExpressionHelpers
    {
        public static string AvgExp(string param) => $"AVG({param})";
        public static string CountExp(string param = "*") => $"COUNT({param})";
        public static string MaxExp(string param) => $"MAX({param})";
        public static string MinExp(string param) => $"MIN({param})";
        public static string SumExp(string param) => $"SUM({param})";
        public static string DistinctExp(string param) => $"DISTINCT({param})";
        public static string AsExp(string field, string @as) =>
            @as != null ? $"{field} AS {@as.WithAliasBrackets()}" : field;

        public static DvlSqlAsExpression AsExp(string @as, IEnumerable<string> @params = null, bool useAs = true) =>
            new DvlSqlAsExpression(@as, @params, useAs);
        
        public static string MonthExp(string param) => $"MONTH({param})";
        public static string DayExp(string param) => $"DAY({param})";
        public static string YearExp(string param) => $"YEAR({param})";
        public static string GetDateExp() => $"GETDATE()";
        public static string IsNullExp(string param1, string param2) => $"ISNULL({param1}, {param2})";

        public static string DateDiffExp(string interval, string starting, string ending) =>
            $"DATEDIFF({interval}, {starting}, {ending})";

        public static string ConvertExp(string type, string name) => $"CONVERT({type}, {name})";

        public static DvlSqlWhereExpression WhereExp(DvlSqlBinaryExpression innerExpression, bool isRoot = false) =>
            new DvlSqlWhereExpression(innerExpression).WithRoot(isRoot);

        public static DvlSqlAndExpression AndExp(params DvlSqlBinaryExpression[] innerExpressions) =>
            new DvlSqlAndExpression(innerExpressions);

        public static DvlSqlAndExpression AndExp(IEnumerable<DvlSqlBinaryExpression> innerExpressions) =>
            new DvlSqlAndExpression(innerExpressions);

        public static DvlSqlComparisonExpression ComparisonExp(DvlSqlConstantExpression leftExp,
            SqlComparisonOperator op,
            DvlSqlConstantExpression rightExp)
            => new DvlSqlComparisonExpression(leftExp, op, rightExp);

        public static DvlSqlConstantExpression<TValue> ConstantExpCol<TValue>(TValue value) =>
            new DvlSqlConstantExpression<TValue>(value, true);

        public static DvlSqlConstantExpression<TValue> ConstantExp<TValue>(TValue value, bool isTableColumn = false) =>
            new DvlSqlConstantExpression<TValue>(value, isTableColumn);

        public static DvlSqlOrExpression OrExp(params DvlSqlBinaryExpression[] innerExpressions) =>
            new DvlSqlOrExpression(innerExpressions);

        public static DvlSqlInExpression InExp(string parameterName, params DvlSqlExpression[] innerExpressions) =>
            new DvlSqlInExpression(parameterName, innerExpressions);

        public static DvlSqlSelectExpression SelectExp(int? topNum = null,
            bool isRoot = false) =>
            new DvlSqlSelectExpression(topNum); //.WithRoot(isRoot);

        public static DvlSqlSelectExpression SelectExp(int? topNum = null,
            params string[] paramNames) =>
            new DvlSqlSelectExpression(paramNames.ToHashSet(), topNum);

        public static DvlSqlSelectExpression SelectExp(IEnumerable<string> paramNames,
            int? topNum = null) =>
            new DvlSqlSelectExpression(paramNames.ToHashSet(), topNum);

        public static DvlSqlFromExpression FromExp(string tableName, bool withNoLock = false) =>
            new DvlSqlFromWithTableExpression(tableName, withNoLock);

        public static DvlSqlFromExpression FromExp(DvlSqlFullSelectExpression select, string @as = null)
        {
            if (@as != null)
                select.As = AsExp(@as);
            return select;
        }

        public static DvlSqlFromExpression FromExp(DvlSqlFromWithTableExpression fromWithTable, string @as = null)
        {
            if (@as != null)
                fromWithTable.As = AsExp(@as);
            return fromWithTable;
        }

        public static DvlSqlFromExpression ValuesExp<T>(T[] values, DvlSqlAsExpression @as) where T : ITuple =>
            new DvlSqlValuesExpression<T>(values, @as);

        public static DvlSqlFromExpression ValuesExp<T>(T[] values, DvlSqlAsExpression @as, List<DvlSqlParameter> sqlParameters) where T : ITuple =>
            new DvlSqlValuesExpression<T>(values, @as, sqlParameters);

        public static DvlSqlSelectExpression SelectExp(params string[] paramNames) =>
            new DvlSqlSelectExpression(paramNames); //.WithRoot(false);

        public static DvlSqlSelectExpression SelectTopExp(int topNum,
            params string[] paramNames) =>
            new DvlSqlSelectExpression(paramNames.ToHashSet(), topNum); //.WithRoot(false);

        public static DvlSqlLikeExpression LikeExp(string field, string pattern) =>
            new DvlSqlLikeExpression(field, pattern);

        public static DvlSqlBinaryExpression NotExp(DvlSqlBinaryExpression binaryExpression) =>
            !binaryExpression;

        public static DvlSqlBinaryExpression
            NotInExp(string parameterName, params DvlSqlExpression[] innerExpressions) =>
            NotExp(InExp(parameterName, innerExpressions));

        public static DvlSqlBinaryExpression NotLikeExp(string field, string pattern) =>
            NotExp(LikeExp(field, pattern));

        public static DvlSqlIsNullExpression IsNullExp(DvlSqlExpression expression) =>
            new DvlSqlIsNullExpression(expression);

        public static DvlSqlBinaryExpression IsNotNullExp(DvlSqlExpression expression) => NotExp(IsNullExp(expression));

        public static DvlSqlFullSelectExpression FullSelectExp(
            DvlSqlSelectExpression @select,
            DvlSqlFromExpression @from,
            DvlSqlAsExpression @as = null,
            List<DvlSqlJoinExpression> @join = null, DvlSqlWhereExpression @where = null,
            DvlSqlGroupByExpression groupBy = null, DvlSqlOrderByExpression orderBy = null,
            DvlSqlSkipExpression skip = null) =>
            new DvlSqlFullSelectExpression(@from, @join, @where, groupBy,
                @select, orderBy, skip, @as);

        public static DvlSqlOrderByExpression OrderByExp(params (string column, Ordering ordering)[] @params) =>
            new DvlSqlOrderByExpression(@params);

        public static DvlSqlExistsExpression ExistsExp(DvlSqlFullSelectExpression select) =>
            new DvlSqlExistsExpression(select);

        public static DvlSqlSkipExpression SkipExp(int offsetRows, int? fetchNextRows = null) =>
            new DvlSqlSkipExpression(offsetRows, fetchNextRows);

        public static DvlSqlGroupByExpression GroupByExp(params string[] paramNames) =>
            new DvlSqlGroupByExpression(paramNames);

        internal static DvlSqlBinaryExpression SetNot(this DvlSqlBinaryExpression binaryExpression, bool not) =>
            not ? !binaryExpression : binaryExpression;

        public static DvlSqlTableDeclarationExpression DeclareTableExp(string name) =>
            new DvlSqlTableDeclarationExpression(name);

        public static DvlSqlOutputExpression
            OutputExp(DvlSqlTableDeclarationExpression intoTable, params string[] cols) =>
            new DvlSqlOutputExpression(intoTable, cols);

        public static DvlSqlOutputExpression OutputExp(params string[] cols) =>
            new DvlSqlOutputExpression(cols);

        public static DvlSqlValuesExpression<T> ValuesExp<T>(params T[] values) where T : ITuple =>
            new DvlSqlValuesExpression<T>(values);
    }
}