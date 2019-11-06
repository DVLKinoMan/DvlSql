using DVL_SQL_Test1.Expressions;
using DVL_SQL_Test1.Models;

namespace DVL_SQL_Test1.Abstract
{
    public interface IUpdateable : IUpdateSetable, IInsertDeleteExecutable
    {
        IInsertDeleteExecutable Where(DvlSqlBinaryExpression binaryExpression, params DvlSqlParameter[] @params);
    }
}
