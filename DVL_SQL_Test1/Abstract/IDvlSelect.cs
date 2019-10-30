using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlSelect
    {
        IDvlOrderBy Select(params string[] parameterNames);
        IDvlOrderBy Select();
        IDvlOrderBy SelectTop(int count, params string[] parameterNames);
        IDvlWhere Where(DvlSqlBinaryExpression binaryExpression);
        IDvlSelect Join(string tableName, DvlSqlComparisonExpression compExpression);
        IDvlSelect FullJoin(string tableName, DvlSqlComparisonExpression compExpression);
        IDvlSelect LeftJoin(string tableName, DvlSqlComparisonExpression compExpression);
        IDvlSelect RightJoin(string tableName, DvlSqlComparisonExpression compExpression);
        IDvlGroupBy GroupBy(params string[] parameterNames);
    }
}
