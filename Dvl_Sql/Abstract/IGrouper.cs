using System.Collections.Generic;
using Dvl_Sql.Expressions;
using Dvl_Sql.Models;

namespace Dvl_Sql.Abstract
{
    public interface IGrouper : ISelectable
    {
        ISelectable Having(DvlSqlBinaryExpression binaryExpression);
        ISelectable Having(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params);
    }
}
