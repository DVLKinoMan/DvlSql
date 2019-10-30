using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlSelector : ISelector
    {
        private readonly string _connectionString;
        private readonly DvlSqlFromExpression _sqlFromExpression;
        private readonly List<DvlSqlJoinExpression> _sqlJoinExpressions = new List<DvlSqlJoinExpression>();
        private DvlSqlWhereExpression _sqlWhereExpression;
        private DvlSqlGroupByExpression _sqlGroupByExpression;
        private DvlSqlSelectExpression _sqlSelectExpression;
        private DvlSqlOrderByExpression _sqlOrderByExpression;

        public SqlSelector(DvlSqlFromExpression sqlFromExpression, string connectionString)
            => (this._sqlFromExpression, this._connectionString) = (sqlFromExpression, connectionString);

        public string GetSqlString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this._sqlSelectExpression.Accept(commandBuilder);
            foreach (var joinExpression in this._sqlJoinExpressions)
                joinExpression.Accept(commandBuilder);
            this._sqlWhereExpression?.Accept(commandBuilder);
            this._sqlGroupByExpression?.Accept(commandBuilder);
            this._sqlOrderByExpression?.Accept(commandBuilder);

            return builder.ToString();
        }

        public SqlSelector WithSelectTop(int num)
        {
            if (this._sqlSelectExpression != null)
                this._sqlSelectExpression.Top = num;
            return this;
        }

        public IOrderer Select(params string[] parameterNames)
        {
            this._sqlSelectExpression = new DvlSqlSelectExpression(this._sqlFromExpression, parameterNames);

            return new SqlOrderer(this, new SqlExecutor(new DvlSqlConnection(this._connectionString), this));
        }

        public IOrderer Select()
        {
            this._sqlSelectExpression = new DvlSqlSelectExpression(this._sqlFromExpression);

            return new SqlOrderer(this, new SqlExecutor(new DvlSqlConnection(this._connectionString), this));
        }

        public IOrderer SelectTop(int count, params string[] parameterNames)
        {
            this._sqlSelectExpression = new DvlSqlSelectExpression(this._sqlFromExpression, parameterNames, count);

            return new SqlOrderer(this, new SqlExecutor(new DvlSqlConnection(this._connectionString), this));
        }

        public IFilter Where(DvlSqlBinaryExpression binaryExpression)
        {
            this._sqlWhereExpression = new DvlSqlWhereExpression(binaryExpression);
            return new SqlFilter(this);
        }

        public ISelector Join(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlInnerJoinExpression(tableName, compExpression));
            return this;
        }

        public ISelector FullJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlInnerJoinExpression(tableName, compExpression));
            return this;
        }

        public ISelector LeftJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlLeftJoinExpression(tableName, compExpression));
            return this;
        }

        public ISelector RightJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlRightJoinExpression(tableName, compExpression));
            return this;
        }

        public IOrderer OrderBy(IOrderer orderBy, params string[] fields)
        {
            if (this._sqlOrderByExpression == null)
                this._sqlOrderByExpression = new DvlSqlOrderByExpression(fields.Select(f => (f, Ascending: Ordering.ASC)));
            else this._sqlOrderByExpression.AddRange(fields.Select(f => (f, Ascending: Ordering.ASC)));

            return orderBy;
        }

        public IOrderer OrderByDescending(IOrderer orderBy, params string[] fields)
        {
            if (this._sqlOrderByExpression == null)
                this._sqlOrderByExpression = new DvlSqlOrderByExpression(fields.Select(f => (f, Descending: Ordering.DESC)));
            else this._sqlOrderByExpression.AddRange(fields.Select(f => (f, Descending: Ordering.DESC)));

            return orderBy;
        }

        public IGrouper GroupBy(params string[] parameterNames)
        {
            this._sqlGroupByExpression = new DvlSqlGroupByExpression(parameterNames);

            return new SqlGrouper(this);
        }
    }
}
