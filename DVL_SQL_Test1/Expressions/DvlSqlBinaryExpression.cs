namespace Dvl_Sql.Expressions
{
    public abstract class DvlSqlBinaryExpression : DvlSqlExpression
    {
        public bool Not { get; set; } = false;

        public static DvlSqlAndExpression operator &(DvlSqlBinaryExpression leftBinaryExpression,
            DvlSqlBinaryExpression rightBinaryExpression) =>
            new DvlSqlAndExpression(leftBinaryExpression, rightBinaryExpression);

        public static DvlSqlOrExpression operator |(DvlSqlBinaryExpression leftBinaryExpression,
            DvlSqlBinaryExpression rightBinaryExpression) =>
            new DvlSqlOrExpression(leftBinaryExpression, rightBinaryExpression);

        public static DvlSqlNotExpression operator !(DvlSqlBinaryExpression binaryExpression) =>
            new DvlSqlNotExpression(binaryExpression);
    }
}
