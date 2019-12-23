﻿using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlNotExpression : DvlSqlBinaryExpression
    {
        public DvlSqlBinaryExpression BinaryExpression;

        public DvlSqlNotExpression(DvlSqlBinaryExpression binaryExpression) =>
            this.BinaryExpression = binaryExpression;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
