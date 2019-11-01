using DVL_SQL_Test1.Abstract;
using System.Collections.Generic;
using DVL_SQL_Test1.Helpers;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlUpdateExpression : DvlSqlExpression
    {
        public string TableName { get; set; }
        public DvlSqlWhereExpression WhereExpression { get; set; }
        public List<(string columnName, string value)> Values { get; set; } = new List<(string columnName, string value)>();

        public DvlSqlUpdateExpression(string tableName) =>
            this.TableName = tableName;

        public void Add<TVal>((string columnName, TVal val) val) =>
            this.Values.Add((val.columnName, DvlSqlHelpers.GetDefaultSqlString(val.val)));

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
