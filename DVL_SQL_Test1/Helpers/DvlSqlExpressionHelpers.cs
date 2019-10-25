using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Helpers
{
    public static class DvlSqlExpressionHelpers
    {
        //public static List<TResult> AsList<TResult>(SqlDataReader reader, Func<IDataRecord, TResult> recordSelector)
        //{
        //    var list =new List<TResult>();
        //    while (reader.Read())
        //    {
        //        list.Add(recordSelector((IDataRecord)reader));
        //    }

        //    return list;
        //}

        public static DvlSqlWhereExpression WhereExp(DvlSqlExpression innerExpression) => new DvlSqlWhereExpression(innerExpression);

        public static DvlSqlAndExpression AndExp(params DvlSqlExpression[] innerExpressions) => new DvlSqlAndExpression(innerExpressions);

        public static DvlSqlComparisonExpression ComparisonExp(DvlSqlExpression leftExp, SqlComparisonOperator op,  DvlSqlExpression rightExp) 
            => new DvlSqlComparisonExpression(leftExp, op, rightExp);

        public static DvlSqlConstantExpression<TValue> ConstantExp<TValue>(TValue value) => new DvlSqlConstantExpression<TValue>(value);

        public static DvlSqlOrExpression OrExp(params DvlSqlExpression[] innerExpressions) => new DvlSqlOrExpression(innerExpressions);

        public static DvlSqlInExpression InExp(string parameterName, params DvlSqlExpression[] innerExpressions) =>
            new DvlSqlInExpression(parameterName, innerExpressions);

        public static DvlSqlSelectExpression SelectExp(DvlSqlFromExpression fromExp)=>new DvlSqlSelectExpression(fromExp);

        public static DvlSqlSelectExpression SelectExp(DvlSqlFromExpression fromExp, params string[] paramNames) =>
            new DvlSqlSelectExpression(fromExp, paramNames);
    }
}
