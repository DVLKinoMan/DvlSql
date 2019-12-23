using System.Collections.Generic;
using Dvl_Sql.Expressions;
using Dvl_Sql.Models;

namespace Dvl_Sql.Abstract
{
    public interface IDeletable : IInsertDeleteExecutable
    {
        IInsertDeleteExecutable Where(DvlSqlBinaryExpression binaryExpression);
        IInsertDeleteExecutable Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params);
    }
}
