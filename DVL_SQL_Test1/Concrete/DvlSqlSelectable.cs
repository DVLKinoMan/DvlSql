using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSqlSelectable : IDvlSelectable
    {
        private readonly DvlSqlFromExpression _sqlFromExpression;
        private readonly List<DvlSqlWhereExpression> _sqlWhereExpressions = new List<DvlSqlWhereExpression>();
        private readonly Task<Func<string, Func<SqlDataReaderType, CancellationToken, Task<SqlDataReader>>>> _readerAsync;

        public DvlSqlSelectable(DvlSqlFromExpression sqlFromExpression,
            Task<Func<string, Func<SqlDataReaderType, CancellationToken, Task<SqlDataReader>>>> readerAsync)
            => (this._sqlFromExpression, this._readerAsync) = (sqlFromExpression, readerAsync);

        public IExecuter Select(params string[] parameterNames)
        {
            throw new NotImplementedException();
        }

        public IExecuter Select()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            var selectExpression = new DvlSqlSelectExpression(this._sqlFromExpression);
            var whereExpression = GetOneWhereExpression(this._sqlWhereExpressions);
            
            selectExpression.Accept(commandBuilder);
            whereExpression.Accept(commandBuilder);

            return new SqlExecutor(await this._readerAsync(builder.ToString()));
        }

        public IDvlSelectable Where(DvlSqlWhereExpression whereExpression)
        {
            this._sqlWhereExpressions.Add(whereExpression);
            return this;
        }

        private static DvlSqlWhereExpression GetOneWhereExpression(IEnumerable<DvlSqlWhereExpression> whereExpressions) =>
            new DvlSqlWhereExpression(new DvlSqlAndExpression(whereExpressions.Select(ex => ex.InnerExpression)));
    }
}
