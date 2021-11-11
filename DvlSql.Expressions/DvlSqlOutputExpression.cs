using DvlSql.Abstract;
using System;

namespace DvlSql.Models
{
    public class DvlSqlOutputExpression : DvlSqlExpression
    {
        public DvlSqlTableDeclarationExpression IntoTable { get; }
        public string[] Columns { get; }

        public DvlSqlOutputExpression(DvlSqlTableDeclarationExpression intoTable, string[] cols)
        {
            IntoTable = intoTable;
            Columns = cols;
        }

        public DvlSqlOutputExpression(string[] cols)
        {
            Columns = cols;
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone()
        {
            throw new NotImplementedException();
        }
    }
}
