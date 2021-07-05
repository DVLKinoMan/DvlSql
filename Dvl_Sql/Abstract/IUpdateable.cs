using DvlSql.Expressions;
using DvlSql.Models;

namespace DvlSql.Abstract
{
    public interface IUpdateable : IUpdateSetable, IInsertDeleteExecutable<int>
    {
        IInsertDeleteExecutable<int> Where(DvlSqlBinaryExpression binaryExpression, params DvlSqlParameter[] @params);
    }
}
