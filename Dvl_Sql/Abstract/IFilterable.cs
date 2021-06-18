using System.Collections.Generic;
using DvlSql.Expressions;
using DvlSql.Models;

namespace DvlSql.Abstract
{
    public interface IFilterable
    {
        IFilter Where(DvlSqlBinaryExpression binaryExpression);
        IFilter Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params);
    }
}
