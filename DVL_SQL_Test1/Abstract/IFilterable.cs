using System.Collections.Generic;
using Dvl_Sql.Expressions;
using Dvl_Sql.Models;

namespace Dvl_Sql.Abstract
{
    public interface IFilterable
    {
        IFilter Where(DvlSqlBinaryExpression binaryExpression);
        IFilter Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params);
    }
}
