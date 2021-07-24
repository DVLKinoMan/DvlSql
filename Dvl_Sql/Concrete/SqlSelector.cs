using System.Collections.Generic;
using System.Linq;
using System.Text;
using DvlSql.Abstract;
using DvlSql.Expressions;
using DvlSql.Models;

using static DvlSql.ExpressionHelpers;

namespace DvlSql.Concrete
{
    internal class SqlSelector : ISelector, IFilter, IGrouper, IUnionable, IFromable
    {
        private readonly IDvlSqlConnection _dvlSqlConnection;
        private readonly DvlSqlUnionExpression _unionExpression = new DvlSqlUnionExpression();
        
        private DvlSqlFullSelectExpression CurrFullSelectExpression => this._unionExpression.Last().Expression;

        public SqlSelector(DvlSqlFromExpression sqlFromExpression, IDvlSqlConnection dvlSqlConnection)
        {
            this._unionExpression.Add(new DvlSqlFullSelectExpression() {From = sqlFromExpression});
            this._dvlSqlConnection = dvlSqlConnection;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this._unionExpression.Accept(commandBuilder);

            return builder.ToString();
        }

        public void NotExpOnFullSelects()
        {
            foreach (var union in _unionExpression)
                if (union.Expression.Where is { } where)
                    union.Expression.Where = !where;
        }

        public ISelector From(string tableName, bool withNoLock = false)
        {
            this.CurrFullSelectExpression.From = FromExp(tableName, withNoLock);

            return this;
        }

        public ISelector From(DvlSqlFullSelectExpression @select)
        {
            this.CurrFullSelectExpression.From = @select;

            return this;
        }

        public ISelector From(DvlSqlFromWithTableExpression fromWithTableExpression)
        {
            this.CurrFullSelectExpression.From = fromWithTableExpression;

            return this;
        }

        public IEnumerable<DvlSqlParameter> GetDvlSqlParameters() => this.CurrFullSelectExpression?.Where?.Parameters;

        public SqlSelector WithSelectTop(int num)
        {
            if (this.CurrFullSelectExpression.Select != null)
                this.CurrFullSelectExpression.Select.Top = num;
            return this;
        }

        public IOrderer Select(params string[] parameterNames)
        {
            if (CurrFullSelectExpression.Select == null)
                this.CurrFullSelectExpression.Select = SelectExp(parameterNames);
            else this.CurrFullSelectExpression.Select.AddRange(parameterNames);

            return new SqlOrderer(this._dvlSqlConnection, this);
        }

        public IOrderer Select()
        {
            this.CurrFullSelectExpression.Select = SelectExp();

            return new SqlOrderer(this._dvlSqlConnection, this);
        }

        public IOrderer SelectTop(int count, params string[] parameterNames)
        {
            this.CurrFullSelectExpression.Select = SelectExp(parameterNames, count);

            return new SqlOrderer(this._dvlSqlConnection, this);
        }

        public IFilter Where(DvlSqlBinaryExpression binaryExpression)
        {
            this.CurrFullSelectExpression.Where = new DvlSqlWhereExpression(binaryExpression);

            return this;
        }

        public IFilter Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params)
        {
            this.CurrFullSelectExpression.Where = new DvlSqlWhereExpression(binaryExpression).WithParameters(@params) as DvlSqlWhereExpression;
            return this;
        }

        public ISelector Join(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this.CurrFullSelectExpression.Join.Add(new DvlSqlInnerJoinExpression(tableName, compExpression));
            return this;
        }

        public ISelector Join(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            this.CurrFullSelectExpression.Join.Add(new DvlSqlInnerJoinExpression(tableName,
                new DvlSqlConstantExpression<string>(firstTableMatchingCol) == new DvlSqlConstantExpression<string>(secondTableMatchingCol)));
            return this;
        }

        public ISelector FullJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this.CurrFullSelectExpression.Join.Add(new DvlSqlFullJoinExpression(tableName, compExpression));
            return this;
        }

        public ISelector FullJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            this.CurrFullSelectExpression.Join.Add(new DvlSqlFullJoinExpression(tableName,
                new DvlSqlConstantExpression<string>(firstTableMatchingCol) == new DvlSqlConstantExpression<string>(secondTableMatchingCol)));
            return this;
        }

        public ISelector LeftJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this.CurrFullSelectExpression.Join.Add(new DvlSqlLeftJoinExpression(tableName, compExpression));
            return this;
        }

        public ISelector LeftJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            this.CurrFullSelectExpression.Join.Add(new DvlSqlLeftJoinExpression(tableName,
                new DvlSqlConstantExpression<string>(firstTableMatchingCol) == new DvlSqlConstantExpression<string>(secondTableMatchingCol)));
            return this;
        }

        public ISelector RightJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this.CurrFullSelectExpression.Join.Add(new DvlSqlRightJoinExpression(tableName, compExpression));
            return this;
        }

        public ISelector RightJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            this.CurrFullSelectExpression.Join.Add(new DvlSqlRightJoinExpression(tableName,
                new DvlSqlConstantExpression<string>(firstTableMatchingCol) == new DvlSqlConstantExpression<string>(secondTableMatchingCol)));
            return this;
        }

        public IOrderer OrderBy(IOrderer orderBy, params string[] fields)
        {
            if (this.CurrFullSelectExpression.OrderBy == null)
                this.CurrFullSelectExpression.OrderBy = new DvlSqlOrderByExpression(fields.Select(f => (f, Ascending: Ordering.ASC)));
            else this.CurrFullSelectExpression.OrderBy.AddRange(fields.Select(f => (f, Ascending: Ordering.ASC)));

            return orderBy;
        }

        public IOrderer OrderByDescending(IOrderer orderBy, params string[] fields)
        {
            if (this.CurrFullSelectExpression.OrderBy == null)
                this.CurrFullSelectExpression.OrderBy = new DvlSqlOrderByExpression(fields.Select(f => (f, Descending: Ordering.DESC)));
            else this.CurrFullSelectExpression.OrderBy.AddRange(fields.Select(f => (f, Descending: Ordering.DESC)));

            return orderBy;
        }

        public ISelectExecutable Skip(IOrderer orderBy, int offsetRows, int? fetchNextRows = null)
        {
            this.CurrFullSelectExpression.Skip = new DvlSqlSkipExpression(offsetRows, fetchNextRows);

            return orderBy;
        }

        public IGrouper GroupBy(params string[] parameterNames)
        {
            this.CurrFullSelectExpression.GroupBy = new DvlSqlGroupByExpression(parameterNames);

            return this;
        }

        public ISelectable Having(DvlSqlBinaryExpression binaryExpression)
        {
            this.CurrFullSelectExpression.GroupBy.BinaryExpression = binaryExpression;

            return this;
        }

        public ISelectable Having(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params)
        {
            this.CurrFullSelectExpression.GroupBy.BinaryExpression = binaryExpression;
            this.CurrFullSelectExpression.GroupBy.WithParameters(@params);

            return this;
        }

        public IFromable Union()
        {
            var last = this._unionExpression.Last();
            this._unionExpression.RemoveAt(this._unionExpression.Count - 1);
            this._unionExpression.Add((last.Expression, UnionType.Union));
            this._unionExpression.Add(new DvlSqlFullSelectExpression());
            
            return this;
        }

        public IFromable UnionAll()
        {
            var last = this._unionExpression.Last();
            this._unionExpression.RemoveAt(this._unionExpression.Count - 1);
            this._unionExpression.Add((last.Expression, UnionType.UnionAll));
            this._unionExpression.Add(new DvlSqlFullSelectExpression());
            
            return this;
        }
    }
}
