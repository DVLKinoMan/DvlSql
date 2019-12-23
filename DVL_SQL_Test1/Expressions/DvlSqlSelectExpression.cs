using System.Collections.Generic;
using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlSelectExpression : DvlSqlExpression
    {
        public int? Top { get; set; }
        public IEnumerable<string> ParameterNames { get; }
        public DvlSqlFromExpression FromExpression { get; }
        public new bool IsRoot { get; set; } = true;

        public DvlSqlSelectExpression(DvlSqlFromExpression expression, int? top = null) => (this.FromExpression, this.Top) = (expression, top);

        public DvlSqlSelectExpression(DvlSqlFromExpression expression, IEnumerable<string> parameterNames, int? top = null) =>
            (this.FromExpression, this.ParameterNames, this.Top) = (expression, parameterNames, top);

        public DvlSqlSelectExpression WithRoot(bool isRoot)
        {
            this.IsRoot = isRoot;
            return this;
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    }
}
