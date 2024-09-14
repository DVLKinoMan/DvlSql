using DvlSql.Expressions;

namespace DvlSql;

internal static class Exts
{
    internal static DvlSqlBinaryExpression SetNot(this DvlSqlBinaryExpression binaryExpression, bool not)
    {
        if (not)
            binaryExpression.Not = !binaryExpression.Not;
        return binaryExpression;
    }
}
