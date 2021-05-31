﻿using System.Collections.Generic;
using System.Linq;
using Dvl_Sql.Abstract;
using Dvl_Sql.Helpers;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlOrExpression : DvlSqlBinaryExpression
    {
        public IEnumerable<DvlSqlBinaryExpression> InnerExpressions { get; }

        public DvlSqlOrExpression(params DvlSqlBinaryExpression[] binaryExpressions) =>
            this.InnerExpressions = binaryExpressions;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => BinaryClone();

        public override DvlSqlBinaryExpression BinaryClone() =>
            new DvlSqlOrExpression(InnerExpressions.Select(inner => inner.BinaryClone()).ToArray())
                .SetNot(Not);

        public override void NotOnThis()
        {
            this.Not = !this.Not;
            foreach (var inner in this.InnerExpressions)
            {
                var something = !inner;
            }
        }
    }
}
