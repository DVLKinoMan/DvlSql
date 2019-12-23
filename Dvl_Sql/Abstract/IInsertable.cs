using System.Runtime.CompilerServices;
using Dvl_Sql.Expressions;
using Dvl_Sql.Models;

namespace Dvl_Sql.Abstract
{
    // ReSharper disable once IdentifierTypo
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IInsertable<TParam> where TParam : ITuple
    {
        IInsertDeleteExecutable Values(params TParam[] @params);
    }

    // ReSharper disable once IdentifierTypo
    public interface IInsertable
    {
        IInsertDeleteExecutable SelectStatement(DvlSqlFullSelectExpression selectExpression, params DvlSqlParameter[] @params);
    }
}
