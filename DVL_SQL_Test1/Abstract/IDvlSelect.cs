using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlSelect
    {
        IDvlSqlExecutor Select(params string[] parameterNames);
        IDvlSqlExecutor Select();
        IDvlSqlExecutor SelectTop(int count, params string[] parameterNames);
        IDvlWhere Where(DvlSqlBinaryExpression binaryExpression);
        IDvlSelect Join(string tableName, DvlSqlComparisonExpression compExpression);
        IDvlSelect FullJoin(string tableName, DvlSqlComparisonExpression compExpression);
        IDvlSelect LeftJoin(string tableName, DvlSqlComparisonExpression compExpression);
        IDvlSelect RightJoin(string tableName, DvlSqlComparisonExpression compExpression);
        IDvlOrderBy OrderBy(params string[] fields);
        IDvlOrderBy OrderByDescending(params string[] fields);
    }
}
