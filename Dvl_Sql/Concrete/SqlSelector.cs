using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dvl_Sql.Abstract;
using Dvl_Sql.Expressions;
using Dvl_Sql.Models;

namespace Dvl_Sql.Concrete
{
    internal class SqlSelector : ISelector, IFilter, IGrouper
    {
        private readonly IDvlSqlConnection _dvlSqlConnection;
        private readonly DvlSqlFullSelectExpression _fullSelectExpression = new DvlSqlFullSelectExpression();

        public SqlSelector(DvlSqlFromExpression sqlFromExpression, IDvlSqlConnection dvlSqlConnection)
            => (this._fullSelectExpression.SqlFromExpression, this._dvlSqlConnection) = (sqlFromExpression, dvlSqlConnection);

        public string GetSqlString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this._fullSelectExpression.Accept(commandBuilder);

            return builder.ToString();
        }

        public IEnumerable<DvlSqlParameter> GetDvlSqlParameters() => this._fullSelectExpression?.SqlWhereExpression?.Parameters;

        public SqlSelector WithSelectTop(int num)
        {
            if (this._fullSelectExpression.SqlSelectExpression != null)
                this._fullSelectExpression.SqlSelectExpression.Top = num;
            return this;
        }

        public IOrderer Select(params string[] parameterNames)
        {
            this._fullSelectExpression.SqlSelectExpression = new DvlSqlSelectExpression(this._fullSelectExpression.SqlFromExpression, parameterNames);

            return new SqlOrderer(this, new SqlSelectExecutor(this._dvlSqlConnection, this));
        }

        public IOrderer Select()
        {
            this._fullSelectExpression.SqlSelectExpression = new DvlSqlSelectExpression(this._fullSelectExpression.SqlFromExpression);

            return new SqlOrderer(this, new SqlSelectExecutor(this._dvlSqlConnection, this));
        }

        public IOrderer SelectTop(int count, params string[] parameterNames)
        {
            this._fullSelectExpression.SqlSelectExpression = new DvlSqlSelectExpression(this._fullSelectExpression.SqlFromExpression, parameterNames, count);

            return new SqlOrderer(this, new SqlSelectExecutor(this._dvlSqlConnection, this));
        }

        public IFilter Where(DvlSqlBinaryExpression binaryExpression)
        {
            this._fullSelectExpression.SqlWhereExpression = new DvlSqlWhereExpression(binaryExpression);
            return this;
        }

        public IFilter Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params)
        {
            this._fullSelectExpression.SqlWhereExpression = new DvlSqlWhereExpression(binaryExpression).WithParameters(@params) as DvlSqlWhereExpression;
            return this;
        }

        public ISelector Join(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._fullSelectExpression.SqlJoinExpressions.Add(new DvlSqlInnerJoinExpression(tableName, compExpression));
            return this;
        }

        public ISelector Join(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            this._fullSelectExpression.SqlJoinExpressions.Add(new DvlSqlInnerJoinExpression(tableName,
                new DvlSqlConstantExpression<string>(firstTableMatchingCol) == new DvlSqlConstantExpression<string>(secondTableMatchingCol)));
            return this;
        }

        public ISelector FullJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._fullSelectExpression.SqlJoinExpressions.Add(new DvlSqlInnerJoinExpression(tableName, compExpression));
            return this;
        }

        public ISelector FullJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            this._fullSelectExpression.SqlJoinExpressions.Add(new DvlSqlInnerJoinExpression(tableName,
                new DvlSqlConstantExpression<string>(firstTableMatchingCol) == new DvlSqlConstantExpression<string>(secondTableMatchingCol)));
            return this;
        }

        public ISelector LeftJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._fullSelectExpression.SqlJoinExpressions.Add(new DvlSqlLeftJoinExpression(tableName, compExpression));
            return this;
        }

        public ISelector LeftJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            this._fullSelectExpression.SqlJoinExpressions.Add(new DvlSqlLeftJoinExpression(tableName,
                new DvlSqlConstantExpression<string>(firstTableMatchingCol) == new DvlSqlConstantExpression<string>(secondTableMatchingCol)));
            return this;
        }

        public ISelector RightJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._fullSelectExpression.SqlJoinExpressions.Add(new DvlSqlRightJoinExpression(tableName, compExpression));
            return this;
        }

        public ISelector RightJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            this._fullSelectExpression.SqlJoinExpressions.Add(new DvlSqlRightJoinExpression(tableName,
                new DvlSqlConstantExpression<string>(firstTableMatchingCol) == new DvlSqlConstantExpression<string>(secondTableMatchingCol)));
            return this;
        }

        public IOrderer OrderBy(IOrderer orderBy, params string[] fields)
        {
            if (this._fullSelectExpression.SqlOrderByExpression == null)
                this._fullSelectExpression.SqlOrderByExpression = new DvlSqlOrderByExpression(fields.Select(f => (f, Ascending: Ordering.ASC)));
            else this._fullSelectExpression.SqlOrderByExpression.AddRange(fields.Select(f => (f, Ascending: Ordering.ASC)));

            return orderBy;
        }

        public IOrderer OrderByDescending(IOrderer orderBy, params string[] fields)
        {
            if (this._fullSelectExpression.SqlOrderByExpression == null)
                this._fullSelectExpression.SqlOrderByExpression = new DvlSqlOrderByExpression(fields.Select(f => (f, Descending: Ordering.DESC)));
            else this._fullSelectExpression.SqlOrderByExpression.AddRange(fields.Select(f => (f, Descending: Ordering.DESC)));

            return orderBy;
        }

        public IGrouper GroupBy(params string[] parameterNames)
        {
            this._fullSelectExpression.SqlGroupByExpression = new DvlSqlGroupByExpression(parameterNames);

            return this;
        }

        public ISelectable Having(DvlSqlBinaryExpression binaryExpression)
        {
            this._fullSelectExpression.SqlGroupByExpression.BinaryExpression = binaryExpression;

            return this;
        }

        public ISelectable Having(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params)
        {
            this._fullSelectExpression.SqlGroupByExpression.BinaryExpression = binaryExpression;
            this._fullSelectExpression.SqlGroupByExpression.WithParameters(@params);

            return this;
        }
    }
}
