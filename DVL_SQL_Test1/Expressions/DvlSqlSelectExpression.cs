using DVL_SQL_Test1.Abstract;
using System.Collections.Generic;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlSelectExpression : DvlSqlExpression
    {
        public int? Top { get; set; }
        public IEnumerable<string> ParameterNames { get; }
        public DvlSqlFromExpression FromExpression { get; }

        public DvlSqlSelectExpression(DvlSqlFromExpression expression) => this.FromExpression = expression;

        public DvlSqlSelectExpression(DvlSqlFromExpression expression, IEnumerable<string> parameterNames) =>
            (this.FromExpression, this.ParameterNames) = (expression, parameterNames);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    }
}
