using System.Collections.Generic;
using System.Linq;
using DvlSql.Abstract;

namespace DvlSql.Expressions
{
    public class DvlSqlSelectExpression : DvlSqlExpression
    {
        public int? Top { get; set; }
        public HashSet<string> ParameterNames { get; } = new HashSet<string>();

        public DvlSqlFromExpression From { get; }
        // public new bool IsRoot { get; private set; } = true;

        public DvlSqlSelectExpression(DvlSqlFromExpression expression, int? top = null) =>
            (this.From, this.Top) = (expression, top);

        public DvlSqlSelectExpression(DvlSqlFromExpression expression, HashSet<string> parameterNames,
            int? top = null) =>
            (this.From, this.ParameterNames, this.Top) = (expression, parameterNames, top);

        public DvlSqlSelectExpression(DvlSqlFromExpression expression, params string[] parameterNames) =>
            (this.From, this.ParameterNames) = (expression, parameterNames.ToHashSet());

        // public DvlSqlSelectExpression WithRoot(bool isRoot)
        // {
        //     this.IsRoot = isRoot;
        //     return this;
        // }

        public bool Add(string paramName) => this.ParameterNames.Add(paramName);

        public void AddRange(IEnumerable<string> paramNames)
        {
            foreach (var paramName in paramNames)
                this.ParameterNames.Add(paramName);
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => SelectClone();

        public DvlSqlSelectExpression SelectClone() => new DvlSqlSelectExpression(From, ParameterNames, Top);
    }
}
