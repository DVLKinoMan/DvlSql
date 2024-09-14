using System.Collections.Generic;
using static System.Exts.Extensions;

namespace DvlSql.Expressions;

public class DvlSqlConstantExpression<TValue>(TValue value) : DvlSqlComparableExpression<TValue>
{
    protected bool Equals(DvlSqlConstantExpression<TValue> other) => EqualityComparer<TValue>.Default.Equals(this.Value, other.Value);

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((DvlSqlConstantExpression<TValue>)obj);
    }

    public override string ToString() => StringValue;

    public override int GetHashCode() => EqualityComparer<TValue>.Default.GetHashCode(Value!);

    public override DvlSqlComparableExpression<TValue> ComparableClone() => new DvlSqlConstantExpression<TValue>(Value);

    private TValue Value { get; init; } = value;

    public string StringValue => this.Value is string ? $"'{this.Value}'" : GetDefaultSqlString(this.Value);

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => ComparableClone();

    public static DvlSqlComparisonExpression<TValue> operator ==(DvlSqlConstantExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<TValue> operator !=(DvlSqlConstantExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<TValue> operator >(DvlSqlConstantExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<TValue> operator >=(DvlSqlConstantExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<TValue> operator <(DvlSqlConstantExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<TValue> operator <=(DvlSqlConstantExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);
}
