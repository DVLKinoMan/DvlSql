using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Abstract
{
    // ReSharper disable once IdentifierTypo
    public interface IInsertable<TParam>
    {
        IInsertExecutable Values(params TParam[] res);
        IInsertExecutable SelectStatement(DvlSqlSelectExpression selectExpression);
    }
}
