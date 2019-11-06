using System.Collections.Generic;
using DVL_SQL_Test1.Expressions;
using DVL_SQL_Test1.Models;

namespace DVL_SQL_Test1.Abstract
{
    public interface IDeletable : IInsertDeleteExecutable
    {
        IInsertDeleteExecutable Where(DvlSqlBinaryExpression binaryExpression);
        IInsertDeleteExecutable Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params);
    }
}
