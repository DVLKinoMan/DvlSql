using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlConstantExpression<TValue> : DvlSqlExpression
    {
        private TValue Value { get; }

        public string StringValue => this.Value.ToString();

        public DvlSqlConstantExpression(TValue value) => this.Value = value;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public static DvlSqlComparisonExpression operator == (DvlSqlConstantExpression<TValue> lhs, DvlSqlConstantExpression<TValue> rhs)
        {
            return new DvlSqlComparisonExpression(lhs, SqlComparisonOperator.Equality, rhs);
        }

        public static DvlSqlComparisonExpression operator !=(DvlSqlConstantExpression<TValue> lhs, DvlSqlConstantExpression<TValue> rhs)
        {
            return new DvlSqlComparisonExpression(lhs, SqlComparisonOperator.Different, rhs);
        }

        public static DvlSqlComparisonExpression operator > (DvlSqlConstantExpression<TValue> lhs, DvlSqlConstantExpression<TValue> rhs)
        {
            return new DvlSqlComparisonExpression(lhs, SqlComparisonOperator.Greater, rhs);
        }

        public static DvlSqlComparisonExpression operator <(DvlSqlConstantExpression<TValue> lhs, DvlSqlConstantExpression<TValue> rhs)
        {
            return new DvlSqlComparisonExpression(lhs, SqlComparisonOperator.Less, rhs);
        }
    }
}
