using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DvlSql.Abstract;
using DvlSql.Models;

namespace DvlSql.Expressions
{
    public abstract class DvlSqlInsertExpression : DvlSqlExpressionWithParameters
    {
        public string TableName { get; set; }

        public string[] Columns { get; set; }

        public DvlSqlOutputExpression OutputExpression { get; set; }
    }

    public class DvlSqlInsertIntoExpression<TParam> : DvlSqlInsertExpression where TParam : ITuple
    {
        public DvlSqlType[] DvlSqlTypes { get; set; }
        public DvlSqlValuesExpression<TParam> ValuesExpression { get; set; }

        public DvlSqlInsertIntoExpression(string tableName, params DvlSqlType[] types)
        {
            this.TableName = tableName;
            this.IsRoot = true;
            this.Columns = types.Select(t => t.Name).ToArray();
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
        public DvlSqlFullSelectExpression SelectExpression { get; set; }

        public DvlSqlInsertIntoSelectExpression(string tableName, IEnumerable<string> columns) =>
            (this.TableName, this.Columns, this.IsRoot) = (tableName, columns.ToArray(), false);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
