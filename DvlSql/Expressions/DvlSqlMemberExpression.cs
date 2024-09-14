namespace DvlSql.Expressions;

public class DvlSqlMemberExpression<TValue>(string memberName) : DvlSqlComparableExpression<TValue>
{
    public string MemberName { get; init; } = memberName;

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null)
            return false;

        throw new System.NotImplementedException();
    }

    public override int GetHashCode()
    {
        throw new System.NotImplementedException();
    }

    public override DvlSqlExpression Clone() => ComparableClone();

    public override DvlSqlComparableExpression<TValue> ComparableClone() => new DvlSqlMemberExpression<TValue>(MemberName);

    public static DvlSqlComparisonExpression<TValue> operator ==(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<TValue> operator !=(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<TValue> operator >(DvlSqlMemberExpression<TValue> lhs,
            DvlSqlMemberExpression<TValue> rhs) =>
            new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<TValue> operator >=(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<TValue> operator <(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<TValue> operator <=(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlMemberExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);

    public static DvlSqlComparisonExpression<TValue> operator ==(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Equality, rhs);

    public static DvlSqlComparisonExpression<TValue> operator !=(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Different, rhs);

    public static DvlSqlComparisonExpression<TValue> operator >(DvlSqlMemberExpression<TValue> lhs,
            DvlSqlConstantExpression<TValue> rhs) =>
            new(lhs, SqlComparisonOperator.Greater, rhs);

    public static DvlSqlComparisonExpression<TValue> operator >=(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

    public static DvlSqlComparisonExpression<TValue> operator <(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.Less, rhs);

    public static DvlSqlComparisonExpression<TValue> operator <=(DvlSqlMemberExpression<TValue> lhs,
        DvlSqlConstantExpression<TValue> rhs) =>
        new(lhs, SqlComparisonOperator.LessOrEqual, rhs);
}
