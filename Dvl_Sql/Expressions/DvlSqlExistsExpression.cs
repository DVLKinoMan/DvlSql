﻿using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlExistsExpression : DvlSqlBinaryExpression
    {
        public DvlSqlFullSelectExpression Select { get; }
        
        public DvlSqlExistsExpression(DvlSqlFullSelectExpression select) => this.Select = select;
            
        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => BinaryClone();

        public override DvlSqlBinaryExpression BinaryClone() => new DvlSqlExistsExpression(Select.FullSelectClone());

        public override void NotOnThis()
        {
            this.Not = !this.Not;
        }
    }
}