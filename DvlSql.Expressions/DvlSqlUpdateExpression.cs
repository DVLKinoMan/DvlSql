﻿using System.Collections.Generic;
using System.Linq;
using DvlSql.Abstract;
using DvlSql.Models;

namespace DvlSql.Models
{
    public class DvlSqlUpdateExpression : DvlSqlExpressionWithParameters
    {
        public string TableName { get; set; }
        public DvlSqlWhereExpression WhereExpression { get; set; }
        public List<DvlSqlParameter> DvlSqlParameters { get; set; } = new List<DvlSqlParameter>();

        public DvlSqlUpdateExpression(string tableName) =>
            this.TableName = tableName;

        public void Add<TVal>(DvlSqlType<TVal> val) => 
            this.DvlSqlParameters.Add(new DvlSqlParameter<TVal>(val.Name, val));

        public void Add(DvlSqlParameter val) =>
            this.DvlSqlParameters.Add(val);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => UpdateClone();

        public DvlSqlUpdateExpression UpdateClone() => new DvlSqlUpdateExpression(TableName)
        {
            WhereExpression = WhereExpression?.WhereClone(),
            DvlSqlParameters = DvlSqlParameters.ToList()
        };
    }
}
