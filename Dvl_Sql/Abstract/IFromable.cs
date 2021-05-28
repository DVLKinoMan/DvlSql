using Dvl_Sql.Expressions;

namespace Dvl_Sql.Abstract
{
    public interface IFromable
    {
        ISelector From(string tableName, bool withNoLock = false);
        ISelector From(DvlSqlFullSelectExpression select, string @as);
    }
}