using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlSelectable
    {
        IExecutor Select(params string[] parameterNames);
        IExecutor Select();
        IDvlSelectable Where(DvlSqlWhereExpression whereExpression);
    }
}
