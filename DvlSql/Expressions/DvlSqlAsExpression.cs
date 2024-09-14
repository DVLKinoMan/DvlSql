using System;
using System.Collections.Generic;

namespace DvlSql.Expressions;

public class DvlSqlAsExpression(string name, bool useAs = true) : DvlSqlExpression
{
    public string Name { get; init; } = name;
    public IEnumerable<string>? Parameters { get; set; }
    public bool UseAsKeyword { get; init; } = useAs;

    public DvlSqlAsExpression(string name, IEnumerable<string>? @params, bool useAs = true) : this(name, useAs)
    {
        Parameters = @params;
    }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone()
    {
        throw new NotImplementedException();
    }
}
