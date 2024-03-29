﻿using System;
using System.Collections.Generic;
using static System.Exts.Extensions;

namespace DvlSql.Expressions
{
    public abstract class DvlSqlConstantExpression : DvlSqlExpression
    {
        protected bool Equals(DvlSqlConstantExpression other) => throw new NotImplementedException();

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((DvlSqlConstantExpression) obj);
        }

        public override int GetHashCode() => throw new System.NotImplementedException();

        public static DvlSqlComparisonExpression operator ==(DvlSqlConstantExpression lhs,
            DvlSqlConstantExpression rhs) =>
            new(lhs, SqlComparisonOperator.Equality, rhs);

        public static DvlSqlComparisonExpression operator !=(DvlSqlConstantExpression lhs,
            DvlSqlConstantExpression rhs) =>
            new(lhs, SqlComparisonOperator.Different, rhs);

        public static DvlSqlComparisonExpression operator >(DvlSqlConstantExpression lhs,
            DvlSqlConstantExpression rhs) =>
            new(lhs, SqlComparisonOperator.Greater, rhs);

        public static DvlSqlComparisonExpression operator >=(DvlSqlConstantExpression lhs,
            DvlSqlConstantExpression rhs) =>
            new(lhs, SqlComparisonOperator.GreaterOrEqual, rhs);

        public static DvlSqlComparisonExpression operator <(DvlSqlConstantExpression lhs,
            DvlSqlConstantExpression rhs) =>
            new(lhs, SqlComparisonOperator.Less, rhs);

        public static DvlSqlComparisonExpression operator <=(DvlSqlConstantExpression lhs,
            DvlSqlConstantExpression rhs) =>
            new(lhs, SqlComparisonOperator.LessOrEqual, rhs);
        
        public static implicit operator DvlSqlConstantExpression(string str) => new DvlSqlConstantExpression<string>(str, true);
        
        public static implicit operator DvlSqlConstantExpression(int num) => new DvlSqlConstantExpression<int>(num);
        
        public static implicit operator DvlSqlConstantExpression(double num) => new DvlSqlConstantExpression<double>(num);
        
        public static implicit operator DvlSqlConstantExpression(DateTime dateTime) => new DvlSqlConstantExpression<DateTime>(dateTime);

        public abstract DvlSqlConstantExpression ConstantClone();
    }

    public class DvlSqlConstantExpression<TValue>(TValue value, bool isTableColumn = true) : DvlSqlConstantExpression
    {
        protected bool Equals(DvlSqlConstantExpression<TValue> other) => EqualityComparer<TValue>.Default.Equals(this.Value, other.Value);

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((DvlSqlConstantExpression<TValue>) obj);
        }

        public override string ToString() => StringValue;

        public override int GetHashCode() => EqualityComparer<TValue>.Default.GetHashCode(Value!);

        public override DvlSqlConstantExpression ConstantClone() => new DvlSqlConstantExpression<TValue>(Value, IsTableColumn);

        private TValue Value { get; init; } = value;

        private bool IsTableColumn { get; init; } = isTableColumn;

        public string StringValue => !IsTableColumn && this.Value is string ? $"'{this.Value}'" : GetDefaultSqlString(this.Value);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => ConstantClone();
    }
}
