using DvlSql.Expressions;

namespace DvlSql;

public interface IDeleteJoinable : IDeleteOutputable<int>, IInsertDeleteExecutable<int>
{
    IDeleteJoinable Join<T>(string tableName, DvlSqlComparisonExpression<T> compExpression);
    IDeleteJoinable Join<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    IDeleteJoinable FullJoin<T>(string tableName, DvlSqlComparisonExpression<T> compExpression);
    IDeleteJoinable FullJoin<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    IDeleteJoinable LeftJoin<T>(string tableName, DvlSqlComparisonExpression<T> compExpression);
    IDeleteJoinable LeftJoin<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    IDeleteJoinable RightJoin<T>(string tableName, DvlSqlComparisonExpression<T> compExpression);
    IDeleteJoinable RightJoin<T>(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
}
