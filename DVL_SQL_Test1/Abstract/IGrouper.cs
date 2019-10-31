using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Abstract
{
    public interface IGrouper : ISelectable
    {
        ISelectable Having(DvlSqlBinaryExpression binaryExpression);
    }
}
