using System.Collections.Generic;
using System.Linq;
using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlSelectExpression : DvlSqlExpression
    {
        public int? Top { get; set; }
        public IEnumerable<string> ParameterNames { get; }

        public DvlSqlFromExpression From { get; }
        // public new bool IsRoot { get; private set; } = true;

        public DvlSqlSelectExpression(DvlSqlFromExpression expression, int? top = null) =>
            (this.From, this.Top) = (expression, top);

        public DvlSqlSelectExpression(DvlSqlFromExpression expression, IEnumerable<string> parameterNames,
            int? top = null) =>
            (this.From, this.ParameterNames, this.Top) = (expression, parameterNames, top);

        public DvlSqlSelectExpression(DvlSqlFromExpression expression, params string[] parameterNames) =>
            (this.From, this.ParameterNames) = (expression, parameterNames);

        // public DvlSqlSelectExpression WithRoot(bool isRoot)
        // {
        //     this.IsRoot = isRoot;
        //     return this;
        // }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => SelectClone();

        public DvlSqlSelectExpression SelectClone() => new DvlSqlSelectExpression(From, ParameterNames.ToArray(), Top);
    }
}
