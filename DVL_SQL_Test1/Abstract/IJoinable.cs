using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Abstract
{
    // ReSharper disable once IdentifierTypo
    public interface IJoinable
    {
        ISelector Join(string tableName, DvlSqlComparisonExpression compExpression);
        ISelector FullJoin(string tableName, DvlSqlComparisonExpression compExpression);
        ISelector LeftJoin(string tableName, DvlSqlComparisonExpression compExpression);
        ISelector RightJoin(string tableName, DvlSqlComparisonExpression compExpression);
    }
}
