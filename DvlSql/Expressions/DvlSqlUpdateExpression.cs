using System;
using System.Collections.Generic;

namespace DvlSql.Expressions;

public class DvlSqlUpdateExpression(string tableName) : DvlSqlExpressionWithParameters
{
    public string TableName { get; init; } = tableName;
    public DvlSqlWhereExpression? WhereExpression { get; set; }
    public List<DvlSqlParameter> DvlSqlParameters { get; set; } = [];

    public void Add<TVal>(DvlSqlType<TVal> val) => 
        this.DvlSqlParameters.Add(new DvlSqlParameter<TVal>(val.Name??throw new ArgumentNullException(nameof(val)), val));

    public void Add(DvlSqlParameter val) =>
        this.DvlSqlParameters.Add(val);

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

    public override DvlSqlExpression Clone() => UpdateClone();

    public DvlSqlUpdateExpression UpdateClone() => new(TableName)
    {
        WhereExpression = WhereExpression?.WhereClone(),
        DvlSqlParameters = [.. DvlSqlParameters]
    };
}
