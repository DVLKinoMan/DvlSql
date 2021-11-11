namespace DvlSql.Models
{
    public static class ExpressionHelpers
    {
        internal static DvlSqlBinaryExpression SetNot(this DvlSqlBinaryExpression binaryExpression, bool not)
        {
            if (not)
                binaryExpression.Not = !binaryExpression.Not;
            return binaryExpression;
        }

    }
}