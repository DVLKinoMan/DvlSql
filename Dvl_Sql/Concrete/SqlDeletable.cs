using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DvlSql.Abstract;
using DvlSql.Expressions;
using DvlSql.Models;

namespace DvlSql.Concrete
{
    internal class SqlDeletable : IDeletable
    {
        private readonly IDvlSqlConnection _dvlSqlConnection;
        private readonly DvlSqlDeleteExpression _deleteExpression;
        private IInsertDeleteExecutable<int> _deleteExecutable;

        public SqlDeletable(DvlSqlFromExpression fromExpression, IDvlSqlConnection dvlSqlConnection)
        {
            (this._deleteExpression, this._dvlSqlConnection) =
                (new DvlSqlDeleteExpression(fromExpression), dvlSqlConnection);
            this._deleteExecutable = new SqlInsertDeleteExecutable<int>(this._dvlSqlConnection,
                ToString, GetDvlSqlParameters,
                (command, timeout, token) => command.ExecuteNonQueryAsync(timeout, token ?? default));
        }

        public async Task<int> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default) =>
            await this._deleteExecutable.ExecuteAsync(timeout, cancellationToken);

        public override string ToString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this._deleteExpression.Accept(commandBuilder);

            return builder.ToString();
        }

        public IInsertDeleteExecutable<int> Where(DvlSqlBinaryExpression binaryExpression)
        {
            this._deleteExpression.WhereExpression = new DvlSqlWhereExpression(binaryExpression);
            return this._deleteExecutable =
                new SqlInsertDeleteExecutable<int>(this._dvlSqlConnection, ToString, GetDvlSqlParameters,
                    (command, timeout, token) => command.ExecuteNonQueryAsync(timeout, token ?? default));
        }

        public IInsertDeleteExecutable<int> Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params)
        {
            this._deleteExpression.WhereExpression = new DvlSqlWhereExpression(binaryExpression).WithParameters(@params) as DvlSqlWhereExpression;
            return this._deleteExecutable =
                new SqlInsertDeleteExecutable<int>(this._dvlSqlConnection, ToString, GetDvlSqlParameters,
                    (command, timeout, token) => command.ExecuteNonQueryAsync(timeout, token ?? default));
        }

        private IEnumerable<DvlSqlParameter> GetDvlSqlParameters() => this._deleteExpression.WhereExpression?.Parameters ?? Enumerable.Empty<DvlSqlParameter>();
    }
}
