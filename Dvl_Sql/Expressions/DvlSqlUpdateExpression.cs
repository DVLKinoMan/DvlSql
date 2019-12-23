﻿using System.Collections.Generic;
using Dvl_Sql.Abstract;
using Dvl_Sql.Models;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlUpdateExpression : DvlSqlExpressionWithParameters
    {
        public string TableName { get; set; }
        public DvlSqlWhereExpression WhereExpression { get; set; }
        public List<DvlSqlParameter> DvlSqlParameters { get; set; } = new List<DvlSqlParameter>();

        public List<string> Columns { get; set; } = new List<string>();

        public DvlSqlUpdateExpression(string tableName) =>
            this.TableName = tableName;

        public void Add<TVal>((string columnName, DvlSqlType<TVal> val) val)
        {
            var (columnName, dvlSqlType) = val;
            this.Columns.Add(columnName);
            this.DvlSqlParameters.Add(new DvlSqlParameter<TVal>(dvlSqlType.Name ?? columnName, dvlSqlType));
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
