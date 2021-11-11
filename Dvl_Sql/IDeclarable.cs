using DvlSql.Expressions;

namespace DvlSql
{
    public interface IDeclarable
    {
        DvlSqlTableDeclarationExpression DeclareTable(string name);
    }
}
