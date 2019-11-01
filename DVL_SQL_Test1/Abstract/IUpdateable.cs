using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Abstract
{
    public interface IUpdateable : IUpdateSetable, IInsertDeleteExecutable
    {
        IInsertDeleteExecutable Where(DvlSqlBinaryExpression binaryExpression);
    }
}
