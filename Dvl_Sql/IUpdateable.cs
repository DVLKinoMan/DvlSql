using DvlSql.Expressions;

namespace DvlSql
{
    public interface IUpdateable : IUpdateSetable, IInsertDeleteExecutable<int>
    {
        IInsertDeleteExecutable<int> Where(DvlSqlBinaryExpression binaryExpression, params DvlSqlParameter[] @params);
    }
}
