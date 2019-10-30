using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Abstract
{
    public interface ISelector
    {
        IOrderer Select(params string[] parameterNames);
        IOrderer Select();
        IOrderer SelectTop(int count, params string[] parameterNames);
        IFilter Where(DvlSqlBinaryExpression binaryExpression);
        ISelector Join(string tableName, DvlSqlComparisonExpression compExpression);
        ISelector FullJoin(string tableName, DvlSqlComparisonExpression compExpression);
        ISelector LeftJoin(string tableName, DvlSqlComparisonExpression compExpression);
        ISelector RightJoin(string tableName, DvlSqlComparisonExpression compExpression);
        IGrouper GroupBy(params string[] parameterNames);
    }
}
