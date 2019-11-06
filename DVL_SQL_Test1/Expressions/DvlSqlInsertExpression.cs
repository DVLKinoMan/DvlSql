﻿using DVL_SQL_Test1.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DVL_SQL_Test1.Models;

namespace DVL_SQL_Test1.Expressions
{
    public abstract class DvlSqlInsertExpression : DvlSqlExpressionWithParameters
    {
        public string TableName { get; set; }

        public string[] Columns { get; set; }
    }

    public class DvlSqlInsertIntoExpression<TParam> : DvlSqlInsertExpression where TParam : ITuple
    {
        public TParam[] Values { get; set; }
        public DvlSqlType[] DvlSqlTypes { get; set; }
        public List<DvlSqlParameter> SqlParameters { get; set; } = new List<DvlSqlParameter>();

        public DvlSqlInsertIntoExpression(string tableName, params (string col, DvlSqlType type)[] types)
        {
            this.TableName = tableName;
            this.IsRoot = true;
            this.Columns = types.Select(t => t.col).ToArray();
            this.DvlSqlTypes = types.Select(t => t.type).ToArray();

            for (int i = 0; i < this.DvlSqlTypes.Length; i++)
                this.DvlSqlTypes[i].Name = this.DvlSqlTypes[i].Name == null
                    ? $"{this.Columns[i]}"
                    : $"{this.DvlSqlTypes[i].Name}";
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }

    public class DvlSqlInsertIntoSelectExpression : DvlSqlInsertExpression
    {
        public DvlSqlFullSelectExpression SelectExpression { get; set; }

        public DvlSqlInsertIntoSelectExpression(string tableName, IEnumerable<string> columns) =>
            (this.TableName, this.Columns, this.IsRoot) = (tableName, columns.ToArray(), false);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
