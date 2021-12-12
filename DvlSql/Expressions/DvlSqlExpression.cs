using System;

namespace DvlSql.Expressions
{
    public enum SqlComparisonOperator
    {
        Equality,
        NotEquality,
        Greater,
        Less,
        GreaterOrEqual,
        LessOrEqual,
        Different,
        NotLess,
        NotGreater
    }

    public enum SqlLogicalOperator
    {
        And,
        Or,
        All,
        Any,
        Between,
        EXISTS,
        IN,
        LIKE,
        NOT,
        ISNULL,
        UNIQUE
    }

    public abstract class DvlSqlExpression
    {
        public bool IsRoot { get; protected set; } = false;
        public abstract void Accept(ISqlExpressionVisitor visitor);
        
        public static implicit operator DvlSqlExpression(string str) => new DvlSqlConstantExpression<string>(str);
        
        public static implicit operator DvlSqlExpression(int num) => new DvlSqlConstantExpression<int>(num);
        
        public static implicit operator DvlSqlExpression(double num) => new DvlSqlConstantExpression<double>(num);
        
        public static implicit operator DvlSqlExpression(DateTime dateTime) => new DvlSqlConstantExpression<DateTime>(dateTime);

        public abstract DvlSqlExpression Clone();
    }
}
