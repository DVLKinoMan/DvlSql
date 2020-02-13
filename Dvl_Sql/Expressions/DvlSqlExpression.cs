using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
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
    }
}
