using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSqlSelectable : IDvlSelectable
    {
        private readonly DvlSqlFromExpression _sqlFromExpression;
        private List<DvlSqlWhereExpression> _sqlWhereExpressions = new List<DvlSqlWhereExpression>();

        public DvlSqlSelectable(DvlSqlFromExpression sqlFromExpression) => this._sqlFromExpression = sqlFromExpression;

        public IExecuter Select(params string[] parameterNames)
        {
            throw new NotImplementedException();
        }

        public IExecuter Select()
        {
            var selectExpression = new DvlSqlSelectExpression(this._sqlFromExpression);
            var whereExpression = _sqlWhereExpressions.
            var connection = new SqlConnection("asdfasdf");
            //await connection.OpenAsync();
            var command = new SqlCommand("asdfasd", connection);
            return new SqlExecutor(command);
        }

        public IDvlSelectable Where(DvlSqlWhereExpression whereExpression)
        {
            this._sqlWhereExpressions.Add(whereExpression);
            return this;
        }

        private DvlSqlWhereExpression GetOneWhereExpression(List<DvlSqlWhereExpression> whereExpressions)
        {
            var res = whereExpressions[0];
            for (int i = 1; i < whereExpressions.Count; i++)
            {
                res.InnerExpression = new DvlSqlAndExpression(res.InnerExpression, whereExpressions[i]);
            }
        }
    }
}
