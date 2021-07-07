using DvlSql.Abstract;
using System;
using System.Collections.Generic;

namespace DvlSql.Expressions
{
    public class DvlSqlAsExpression : DvlSqlExpression
    {
        public string Name { get; set; }
        public IEnumerable<string> Parameters { get; set; }
        public bool UseAsKeyword { get; set; }

        public DvlSqlAsExpression(string name, bool useAs = true)
        {
            Name = name;
            UseAsKeyword = useAs;
        }

        public DvlSqlAsExpression(string name, IEnumerable<string> @params, bool useAs = true) : this(name, useAs)
        {
            Parameters = @params;
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone()
        {
            throw new NotImplementedException();
        }
    }
}
