﻿using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlDeletable : IDeletable
    {
        private readonly string _connectionString;
        private readonly DvlSqlDeleteExpression _deleteExpression;
        private IInsertDeleteExecutable _deleteExecutable;

        public SqlDeletable(DvlSqlFromExpression fromExpression, string connectionString) =>
            (this._deleteExpression, this._connectionString) =
            (new DvlSqlDeleteExpression(fromExpression), connectionString);

        public Task<int> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default) =>
            this._deleteExecutable.ExecuteAsync(timeout, cancellationToken);

        private string GetSqlString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this._deleteExpression.Accept(commandBuilder);

            return builder.ToString();
        }

        public IInsertDeleteExecutable Where(DvlSqlBinaryExpression binaryExpression)
        {
            this._deleteExpression.WhereExpression = new DvlSqlWhereExpression(binaryExpression);
            return this._deleteExecutable =
                new SqlInsertDeleteExecutable(new DvlSqlConnection(this._connectionString), GetSqlString);
        }
    }
}
