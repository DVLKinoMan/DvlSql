﻿using System.Collections.Generic;
using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlOrExpression : DvlSqlBinaryExpression
    {
        public IEnumerable<DvlSqlBinaryExpression> InnerExpressions { get; }

        public DvlSqlOrExpression(params DvlSqlBinaryExpression[] binaryExpressions) =>
            this.InnerExpressions = binaryExpressions;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

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
