﻿using DvlSql.Abstract;


namespace DvlSql.Expressions
{
    public class DvlSqlExistsExpression : DvlSqlBinaryExpression
    {
        public DvlSqlFullSelectExpression Select { get; }
        
        public DvlSqlExistsExpression(DvlSqlFullSelectExpression select) => this.Select = select;
            
        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone() => BinaryClone();

        public override DvlSqlBinaryExpression BinaryClone() => 
            new DvlSqlExistsExpression(Select.FullSelectClone())
            .SetNot(Not);

        public override void NotOnThis()
        {
            this.Not = !this.Not;
        }
    }
}