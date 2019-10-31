using DVL_SQL_Test1.Abstract;
using System.Collections.Generic;

namespace DVL_SQL_Test1.Expressions
{
    public abstract class DvlSqlInsertExpression : DvlSqlExpression
    {
        public string TableName { get; set; }

        public IEnumerable<string> Columns { get; set; }

    }

    public class DvlSqlInsertIntoExpression<TParam> : DvlSqlInsertExpression
    {
        public IEnumerable<TParam> Values { get; set; }

        public DvlSqlInsertIntoExpression(string tableName, IEnumerable<string> columns, IEnumerable<TParam> values) =>
            (this.TableName, this.Columns, this.Values, this.IsRoot) = (tableName, columns, values, true);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }

    public class DvlSqlInsertIntoSelectExpression : DvlSqlInsertExpression
    {
        public DvlSqlSelectExpression SelectExpression { get; set; }

        public DvlSqlInsertIntoSelectExpression(string tableName, IEnumerable<string> columns, DvlSqlSelectExpression selectExpression) =>
            (this.TableName, this.Columns, this.SelectExpression, this.IsRoot) = (tableName, columns, selectExpression, false);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
