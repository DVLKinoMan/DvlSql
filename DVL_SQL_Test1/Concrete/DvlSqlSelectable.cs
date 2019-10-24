using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSqlSelectable : IDvlSelectable
    {
        private readonly DvlSqlFromExpression _sqlFromExpression;
        private readonly List<DvlSqlWhereExpression> _sqlWhereExpressions = new List<DvlSqlWhereExpression>();
        private readonly Func<string, DvlSqlCommand> _funcCommand;

        public DvlSqlSelectable(DvlSqlFromExpression sqlFromExpression,
            Func<string,DvlSqlCommand> funcCommand)
            => (this._sqlFromExpression, this._funcCommand) = (sqlFromExpression, funcCommand);

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

            return new SqlExecutor(this._funcCommand(builder.ToString()));
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
