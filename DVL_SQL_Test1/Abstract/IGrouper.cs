using System.Collections.Generic;
using DVL_SQL_Test1.Expressions;
using DVL_SQL_Test1.Models;

namespace DVL_SQL_Test1.Abstract
{
    public interface IGrouper : ISelectable
    {
        ISelectable Having(DvlSqlBinaryExpression binaryExpression);
        ISelectable Having(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params);
    }
}
