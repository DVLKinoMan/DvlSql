using System.Collections.Generic;

namespace DvlSql.Expressions;

public class DvlSqlSelectExpression : DvlSqlExpression
{
    public int? Top { get; set; }
    public List<string> ParameterNames { get; init; } = [];

    //public DvlSqlFromExpression From { get; }
    // public new bool IsRoot { get; private set; } = true;

    public DvlSqlSelectExpression(int? top = null) =>
        this.Top = top;

    public DvlSqlSelectExpression(List<string> parameterNames,
        int? top = null) =>
        (this.ParameterNames, this.Top) = (parameterNames, top);

    public DvlSqlSelectExpression(params string[] parameterNames) =>
        (this.ParameterNames) = [.. parameterNames];

    // public DvlSqlSelectExpression WithRoot(bool isRoot)
    // {
    //     this.IsRoot = isRoot;
    //     return this;
    // }

    public void Add(string paramName) => this.ParameterNames.Add(paramName);

    public void AddRange(IEnumerable<string> paramNames)
    {
        foreach (var paramName in paramNames)
            this.ParameterNames.Add(paramName);
    }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => SelectClone();

    public DvlSqlSelectExpression SelectClone() => new(ParameterNames, Top);
}