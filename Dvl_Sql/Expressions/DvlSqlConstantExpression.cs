using System.Collections.Generic;
using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
{
    public abstract class DvlSqlConstantExpression : DvlSqlExpression
    {
        protected bool Equals(DvlSqlConstantExpression other) => throw new System.NotImplementedException();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((DvlSqlConstantExpression) obj);
        }

        public override int GetHashCode() => throw new System.NotImplementedException();

        public static DvlSqlComparisonExpression operator ==(DvlSqlConstantExpression lhs,
            DvlSqlConstantExpression rhs) =>
            new DvlSqlComparisonExpression(lhs, SqlComparisonOperator.Equality, rhs);

        public static DvlSqlComparisonExpression operator !=(DvlSqlConstantExpression lhs,
            DvlSqlConstantExpression rhs) =>
            new DvlSqlComparisonExpression(lhs, SqlComparisonOperator.Different, rhs);

        public static DvlSqlComparisonExpression operator >(DvlSqlConstantExpression lhs,
            DvlSqlConstantExpression rhs) =>
            new DvlSqlComparisonExpression(lhs, SqlComparisonOperator.Greater, rhs);

        public static DvlSqlComparisonExpression operator >=(DvlSqlConstantExpression lhs,
            DvlSqlConstantExpression rhs) =>
            new DvlSqlComparisonExpression(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

        public static DvlSqlComparisonExpression operator <(DvlSqlConstantExpression lhs,
            DvlSqlConstantExpression rhs) =>
            new DvlSqlComparisonExpression(lhs, SqlComparisonOperator.Less, rhs);

        public static DvlSqlComparisonExpression operator <=(DvlSqlConstantExpression lhs,
            DvlSqlConstantExpression rhs) =>
            new DvlSqlComparisonExpression(lhs, SqlComparisonOperator.LessOrEqual, rhs);
        
        public static implicit operator DvlSqlConstantExpression(string str) => new DvlSqlConstantExpression<string>(str);
    }

    public class DvlSqlConstantExpression<TValue> : DvlSqlConstantExpression
    {
        protected bool Equals(DvlSqlConstantExpression<TValue> other) => EqualityComparer<TValue>.Default.Equals(this.Value, other.Value);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((DvlSqlConstantExpression<TValue>) obj);
        }

        public override int GetHashCode() => EqualityComparer<TValue>.Default.GetHashCode(Value);

        private TValue Value { get; }

        public string StringValue => this.Value.ToString();

        public DvlSqlConstantExpression(TValue value) => this.Value = value;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
        
    }
}
