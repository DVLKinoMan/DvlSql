using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Models;
using System.Collections.Generic;

namespace DVL_SQL_Test1.Expressions
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
