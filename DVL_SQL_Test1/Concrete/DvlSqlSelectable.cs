using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSqlSelectable : IDvlSelectable
    {
        private readonly DvlSqlFromExpression _sqlFromExpression;
        private readonly List<DvlSqlWhereExpression> _sqlWhereExpressions = new List<DvlSqlWhereExpression>();
        private readonly List<DvlSqlJoinExpression> _sqlJoinExpressions = new List<DvlSqlJoinExpression>();

        private DvlSqlSelectExpression _sqlSelectExpression;
        private readonly string _connectionString;

        public DvlSqlSelectable(DvlSqlFromExpression sqlFromExpression, string connectionString)
            => (this._sqlFromExpression, this._connectionString) = (sqlFromExpression, connectionString);

        public string GetSqlString(DvlSqlOrderByExpression orderByExpression = null)
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            var whereExpression = GetOneWhereExpression(this._sqlWhereExpressions);

            this._sqlSelectExpression.Accept(commandBuilder);
            foreach (var joinExpression in this._sqlJoinExpressions)
                joinExpression.Accept(commandBuilder);
            whereExpression.Accept(commandBuilder);
            orderByExpression?.Accept(commandBuilder);

            return builder.ToString();
        }

        public IExecutor Select(params string[] parameterNames)
        {
            this._sqlSelectExpression = new DvlSqlSelectExpression(this._sqlFromExpression, parameterNames);

            return new SqlExecutor(new DvlSqlConnection(this._connectionString), this);
        }

        public IExecutor Select()
        {
            this._sqlSelectExpression = new DvlSqlSelectExpression(this._sqlFromExpression);

            return new SqlExecutor(new DvlSqlConnection(this._connectionString), this);
        }

        public IDvlSelectable Where(DvlSqlBinaryExpression binaryExpression)
        {
            this._sqlWhereExpressions.Add(new DvlSqlWhereExpression(binaryExpression));
            return this;
        }

        public IDvlSelectable Join(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlInnerJoinExpression(tableName, compExpression));
            return this;
        }

        public IDvlSelectable FullJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlInnerJoinExpression(tableName, compExpression));
            return this;
        }

        public IDvlSelectable LeftJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlLeftJoinExpression(tableName, compExpression));
            return this;
        }

        public IDvlSelectable RightJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlRightJoinExpression(tableName, compExpression));
            return this;
        }

        private static DvlSqlWhereExpression
            GetOneWhereExpression(IEnumerable<DvlSqlWhereExpression> whereExpressions) =>
            new DvlSqlWhereExpression(new DvlSqlAndExpression(whereExpressions.Select(ex => ex.InnerExpression)));

    }
}
