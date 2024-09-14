using DvlSql.Expressions;
using System.Collections.Generic;

namespace DvlSql;

public interface IGrouper : ISelectable
{
    ISelectable Having(DvlSqlBinaryExpression binaryExpression);
    ISelectable Having(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params);
}
