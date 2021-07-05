using DvlSql.Expressions;

namespace DvlSql.Abstract
{
    public interface IDeclarable
    {
        DvlSqlTableDeclarationExpression DeclareTable(string name);
    }
}
