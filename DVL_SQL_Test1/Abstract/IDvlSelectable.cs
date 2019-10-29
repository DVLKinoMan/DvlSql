using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlSelectable
    {
        IExecutor Select(int? topNum = null, params string[] parameterNames);
        IExecutor Select(int? topNum = null);
        IWhereable Where(DvlSqlBinaryExpression binaryExpression);
        IDvlSelectable Join(string tableName, DvlSqlComparisonExpression compExpression);
        IDvlSelectable FullJoin(string tableName, DvlSqlComparisonExpression compExpression);
        IDvlSelectable LeftJoin(string tableName, DvlSqlComparisonExpression compExpression);
        IDvlSelectable RightJoin(string tableName, DvlSqlComparisonExpression compExpression);
        IOrderable OrderBy(params string[] fields);
        IOrderable OrderByDescending(params string[] fields);
    }
}
