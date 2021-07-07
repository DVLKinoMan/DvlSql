using DvlSql.Abstract;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DvlSql.Models;

namespace DvlSql.Expressions
{
    public class DvlSqlValuesExpression<T> : DvlSqlFromExpression where T: ITuple
    {
        public T[] Values { get; set; }
        public List<DvlSqlParameter> SqlParameters { get; set; } = new List<DvlSqlParameter>();

        public DvlSqlValuesExpression(T[] values)
        {
            Values = values;
        }

        public DvlSqlValuesExpression(T[] values, DvlSqlAsExpression @as) : this(values)
        {
            As = @as;
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone()
        {
            throw new NotImplementedException();
        }
    }
}
