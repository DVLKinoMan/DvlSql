﻿using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlComparisonExpression : DvlSqlBinaryExpression
    {
        public DvlSqlConstantExpression LeftExpression { get; }
        public SqlComparisonOperator ComparisonOperator { get; }
        public DvlSqlConstantExpression RightExpression { get; }

        public DvlSqlComparisonExpression(DvlSqlConstantExpression leftExpression, SqlComparisonOperator comparisonOperator,
            DvlSqlConstantExpression rightExpression) => (this.LeftExpression, this.ComparisonOperator, this.RightExpression) =
            (leftExpression, comparisonOperator, rightExpression);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
