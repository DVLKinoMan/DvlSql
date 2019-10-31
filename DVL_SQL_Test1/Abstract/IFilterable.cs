using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Abstract
{
    public interface IFilterable
    {
        IFilter Where(DvlSqlBinaryExpression binaryExpression);
    }
}
