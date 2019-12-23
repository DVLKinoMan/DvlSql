using Dvl_Sql.Expressions;
using Dvl_Sql.Models;

namespace Dvl_Sql.Abstract
{
    public interface IUpdateable : IUpdateSetable, IInsertDeleteExecutable
    {
        IInsertDeleteExecutable Where(DvlSqlBinaryExpression binaryExpression, params DvlSqlParameter[] @params);
    }
}
