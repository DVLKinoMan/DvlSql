using DVL_SQL_Test1.Abstract;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DVL_SQL_Test1.Expressions
{
    public abstract class DvlSqlInsertExpression : DvlSqlExpression
    {
        public string TableName { get; set; }

        public IEnumerable<string> Columns { get; set; }
    }

    public class DvlSqlInsertIntoExpression<TParam> : DvlSqlInsertExpression where TParam : ITuple
    {
        public IEnumerable<TParam> Values { get; set; }

        public DvlSqlInsertIntoExpression(string tableName, IEnumerable<string> columns) =>
            (this.TableName, this.Columns, this.IsRoot) = (tableName, columns, true);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }

    public class DvlSqlInsertIntoSelectExpression : DvlSqlInsertExpression
    {
        public DvlSqlFullSelectExpression SelectExpression { get; set; }

        public DvlSqlInsertIntoSelectExpression(string tableName, IEnumerable<string> columns) =>
            (this.TableName, this.Columns, this.IsRoot) = (tableName, columns, false);

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
