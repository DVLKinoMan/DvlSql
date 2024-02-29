using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DvlSql.Expressions
{
    public abstract class DvlSqlInsertExpression(string tableName, params string[] cols) : DvlSqlExpressionWithParameters
    {
        public string TableName { get; init; } = tableName;

        public string[] Columns { get; init; } = cols;

        public DvlSqlOutputExpression? OutputExpression { get; set; }
    }

    public class DvlSqlInsertIntoExpression<TParam> : DvlSqlInsertExpression where TParam : ITuple
    {
        public DvlSqlType[] DvlSqlTypes { get; set; }

        public DvlSqlValuesExpression<TParam> ValuesExpression { get; set; } = default!;

        public DvlSqlInsertIntoExpression(string tableName, params DvlSqlType[] types) : base(tableName, types.Select(t => t.Name??throw new ArgumentNullException(nameof(t.Name))).ToArray())
        {
            this.IsRoot = true;
            this.DvlSqlTypes = types;

            //for (int i = 0; i < this.DvlSqlTypes.Length; i++)
            //    this.DvlSqlTypes[i].Name = this.DvlSqlTypes[i].Name == null
            //        ? $"{this.Columns[i]}"
            //        : $"{this.DvlSqlTypes[i].Name}";
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }

    public class DvlSqlInsertIntoSelectExpression : DvlSqlInsertExpression
    {
        public DvlSqlFullSelectExpression SelectExpression { get; set; } = default!;

        public DvlSqlInsertIntoSelectExpression(string tableName, IEnumerable<string> columns) : base(tableName, columns.ToArray())
        {
            this.IsRoot = false;
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);

        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
