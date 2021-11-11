using DvlSql.Models;

namespace DvlSql.Abstract
{
    public interface IDeclarable
    {
        DvlSqlTableDeclarationExpression DeclareTable(string name);
    }
}
