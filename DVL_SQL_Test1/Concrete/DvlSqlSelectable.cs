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
        private readonly string _connectionString;

        public DvlSqlSelectable(DvlSqlFromExpression sqlFromExpression, string connectionString)
            => (this._sqlFromExpression, this._connectionString) = (sqlFromExpression, connectionString);

        public IExecutor Select(params string[] parameterNames)
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            var selectExpression = new DvlSqlSelectExpression(this._sqlFromExpression, parameterNames);
            var whereExpression = GetOneWhereExpression(this._sqlWhereExpressions);

            selectExpression.Accept(commandBuilder);
            whereExpression.Accept(commandBuilder);

            return new SqlExecutor(new DvlSqlConnection(this._connectionString, builder.ToString()));
        }

        public IExecutor Select()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            var selectExpression = new DvlSqlSelectExpression(this._sqlFromExpression);
            var whereExpression = GetOneWhereExpression(this._sqlWhereExpressions);
            
            selectExpression.Accept(commandBuilder);
            whereExpression.Accept(commandBuilder);

            return new SqlExecutor(new DvlSqlConnection(this._connectionString,builder.ToString()));
        }

        public IDvlSelectable Where(DvlSqlBinaryExpression binaryExpression)
        {
            this._sqlWhereExpressions.Add(new DvlSqlWhereExpression(binaryExpression));
            return this;
        }

        private static DvlSqlWhereExpression GetOneWhereExpression(IEnumerable<DvlSqlWhereExpression> whereExpressions) =>
            new DvlSqlWhereExpression(new DvlSqlAndExpression(whereExpressions.Select(ex => ex.InnerExpression)));
    }
}
