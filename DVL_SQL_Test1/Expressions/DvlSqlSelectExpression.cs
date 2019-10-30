using DVL_SQL_Test1.Abstract;
using System.Collections.Generic;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlSelectExpression : DvlSqlExpression
    {
        public int? Top { get; set; }
        public IEnumerable<string> ParameterNames { get; }
        public DvlSqlFromExpression FromExpression { get; }
        public new bool IsRoot { get; set; } = false;

        public DvlSqlSelectExpression(DvlSqlFromExpression expression, int? top = null) => (this.FromExpression, this.Top) = (expression, top);

        public DvlSqlSelectExpression(DvlSqlFromExpression expression, IEnumerable<string> parameterNames, int? top = null) =>
            (this.FromExpression, this.ParameterNames, this.Top) = (expression, parameterNames, top);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    }
}
