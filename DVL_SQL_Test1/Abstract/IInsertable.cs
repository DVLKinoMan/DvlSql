using System.Runtime.CompilerServices;
using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Abstract
{
    // ReSharper disable once IdentifierTypo
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IInsertable<TParam> where TParam : ITuple
    {
        IInsertDeleteExecutable Values(params TParam[] res);
    }

    // ReSharper disable once IdentifierTypo
    public interface IInsertable
    {
        IInsertDeleteExecutable SelectStatement(DvlSqlFullSelectExpression selectExpression);
    }
}
