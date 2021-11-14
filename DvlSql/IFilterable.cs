using DvlSql.Expressions;
using System.Collections.Generic;

namespace DvlSql
{
    public interface IFilterable
    {
        IFilter Where(DvlSqlBinaryExpression binaryExpression);
        IFilter Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params);
    }
}
