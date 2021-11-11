using DvlSql.Abstract;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DvlSql.Models;

namespace DvlSql.Models
{
    public abstract class DvlSqlValuesExpression : DvlSqlFromExpression
    {
        public List<DvlSqlParameter> SqlParameters { get; set; } = new List<DvlSqlParameter>();
    }

    public class DvlSqlValuesExpression<T> : DvlSqlValuesExpression  where T: ITuple
    {
        public T[] Values { get; set; }

        public DvlSqlValuesExpression(T[] values)
        {
            Values = values;
        }

        public DvlSqlValuesExpression(T[] values, DvlSqlAsExpression @as) : this(values)
        {
            As = @as;
        }

        public DvlSqlValuesExpression(T[] values, DvlSqlAsExpression @as, List<DvlSqlParameter> sqlParameters) : this(values)
        {
            As = @as;
            SqlParameters = sqlParameters;
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone()
        {
            throw new NotImplementedException();
        }
    }
}
