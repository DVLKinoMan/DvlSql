using System;
using System.Collections.Generic;
using static System.Exts.Extensions;

namespace DvlSql.Expressions
{
    public class DvlSqlTableDeclarationExpression : DvlSqlExpression
    {
        public string TableName { get; set; }
        public List<DvlSqlType> Columns { get; set; }

        public DvlSqlTableDeclarationExpression(string tableName)
        {
            TableName = tableName.WithAlpha();
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone()
        {
            throw new NotImplementedException();
        }

        public DvlSqlTableDeclarationExpression AddColumn(DvlSqlType col)
        {
            Columns ??= new List<DvlSqlType>();
            Columns.Add(col);
            return this;
        }

        public DvlSqlTableDeclarationExpression AddColumns(params DvlSqlType[] cols)
        {
            foreach (var col in cols)
                AddColumn(col);
            return this;
        }
    }
}
