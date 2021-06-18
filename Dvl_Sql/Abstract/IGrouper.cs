using System.Collections.Generic;
using DvlSql.Expressions;
using DvlSql.Models;

namespace DvlSql.Abstract
{
    public interface IGrouper : ISelectable
    {
        ISelectable Having(DvlSqlBinaryExpression binaryExpression);
        ISelectable Having(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params);
    }
}
