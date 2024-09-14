using System;

namespace DvlSql.Expressions;

public class DvlSqlOutputExpression : DvlSqlExpression
{
    public DvlSqlTableDeclarationExpression? IntoTable { get; init; }
    public string[] Columns { get; init; }

    public DvlSqlOutputExpression(DvlSqlTableDeclarationExpression intoTable, string[] cols)
    {
        IntoTable = intoTable;
        Columns = cols;
    }

    public DvlSqlOutputExpression(string[] cols)
    {
        Columns = cols;
    }

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone()
    {
        throw new NotImplementedException();
    }
}
