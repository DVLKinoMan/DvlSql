using DvlSql.Expressions;

namespace DvlSql.Abstract
{
    public interface IFromable
    {
        ISelector From(string tableName, bool withNoLock = false);
        ISelector From(DvlSqlFullSelectExpression select, string @as);
    }
}