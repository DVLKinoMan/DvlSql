using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace DVL_SQL_Test1.Concrete
{
    // ReSharper disable once IdentifierTypo
    public class SqlInsertable<TParam> : IInsertable<TParam> where TParam : ITuple
    {
        private readonly DvlSqlInsertIntoExpression<TParam> _insertExpression;
        private readonly string _connectionString;

        // ReSharper disable once IdentifierTypo
        public SqlInsertable(DvlSqlInsertIntoExpression<TParam> insertExpression, string connectionString) =>
            (this._insertExpression, this._connectionString) = (insertExpression, connectionString);

        public IInsertExecutable Values(params TParam[] res)
        {
            this._insertExpression.Values = res;

            return new SqlInsertExecutable(new DvlSqlConnection(this._connectionString), GetSqlString);
        }

        private string GetSqlString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this._insertExpression.Accept(commandBuilder);

            return builder.ToString();
        }
    }

    // ReSharper disable once IdentifierTypo
    public class SqlInsertable : IInsertable
    {
        private readonly DvlSqlInsertIntoSelectExpression _insertWithSelectExpression;
        private readonly string _connectionString;

        // ReSharper disable once IdentifierTypo
        public SqlInsertable(DvlSqlInsertIntoSelectExpression insertExpression, string connectionString) =>
            (this._insertWithSelectExpression, this._connectionString) = (insertExpression, connectionString);

        public IInsertExecutable SelectStatement(DvlSqlFullSelectExpression selectExpression)
        {
            this._insertWithSelectExpression.SelectExpression = selectExpression;

            return new SqlInsertExecutable(new DvlSqlConnection(this._connectionString), GetSqlString);
        }

        private string GetSqlString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this._insertWithSelectExpression.Accept(commandBuilder);

            return builder.ToString();
        }
    }
}
