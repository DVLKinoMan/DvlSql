using System;

namespace DvlSql.Expressions;

public abstract class DvlSqlComparableExpression : DvlSqlExpression
{
    public static implicit operator DvlSqlComparableExpression(string str) => new DvlSqlConstantExpression<string>(str);

    public static implicit operator DvlSqlComparableExpression(int num) => new DvlSqlConstantExpression<int>(num);

    public static implicit operator DvlSqlComparableExpression(double num) => new DvlSqlConstantExpression<double>(num);

    public static implicit operator DvlSqlComparableExpression(DateTime dateTime) => new DvlSqlConstantExpression<DateTime>(dateTime);
}

public abstract class DvlSqlComparableExpression<T> : DvlSqlComparableExpression
{
    public abstract DvlSqlComparableExpression<T> ComparableClone();
}
