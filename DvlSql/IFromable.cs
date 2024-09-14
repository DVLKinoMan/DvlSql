using DvlSql.Expressions;

namespace DvlSql;

public interface IFromable
{
    ISelector From(string tableName, bool withNoLock = false);
    ISelector From(DvlSqlFullSelectExpression select);
    ISelector From(DvlSqlFromWithTableExpression fromWithTableExpression);
}