using System.Collections.Generic;
using DvlSql.Expressions;
using DvlSql.Models;

namespace DvlSql.Abstract
{
    public interface IDeletable : IInsertDeleteExecutable<int>
    {
        IInsertDeleteExecutable<int> Where(DvlSqlBinaryExpression binaryExpression);
        IInsertDeleteExecutable<int> Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params);
    }
}
