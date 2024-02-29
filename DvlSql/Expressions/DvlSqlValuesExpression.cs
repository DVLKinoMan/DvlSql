﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DvlSql.Expressions
{
    public abstract class DvlSqlValuesExpression : DvlSqlFromExpression
    {
        public List<DvlSqlParameter> SqlParameters { get; init; } = new List<DvlSqlParameter>();
    }

    public class DvlSqlValuesExpression<T> : DvlSqlValuesExpression  where T: ITuple
    {
        public T[] Values { get; init; }

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
