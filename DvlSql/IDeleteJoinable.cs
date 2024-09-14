using DvlSql.Expressions;

namespace DvlSql;

public interface IDeleteJoinable : IDeleteOutputable<int>, IInsertDeleteExecutable<int>
{
    IDeleteJoinable Join(string tableName, DvlSqlComparisonExpression compExpression);
    IDeleteJoinable Join(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    IDeleteJoinable FullJoin(string tableName, DvlSqlComparisonExpression compExpression);
    IDeleteJoinable FullJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    IDeleteJoinable LeftJoin(string tableName, DvlSqlComparisonExpression compExpression);
    IDeleteJoinable LeftJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    IDeleteJoinable RightJoin(string tableName, DvlSqlComparisonExpression compExpression);
    IDeleteJoinable RightJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
}
