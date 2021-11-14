using DvlSql.Expressions;

namespace DvlSql
{
    // ReSharper disable once IdentifierTypo
    public interface IJoinable
    {
        ISelector Join(string tableName, DvlSqlComparisonExpression compExpression);
        ISelector Join(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
        ISelector FullJoin(string tableName, DvlSqlComparisonExpression compExpression);
        ISelector FullJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
        ISelector LeftJoin(string tableName, DvlSqlComparisonExpression compExpression);
        ISelector LeftJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
        ISelector RightJoin(string tableName, DvlSqlComparisonExpression compExpression);
        ISelector RightJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    }
}
