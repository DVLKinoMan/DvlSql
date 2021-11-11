using DvlSql.Expressions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DvlSql.SqlServer
{
    internal class SqlUpdateable : IUpdateable
    {
        private readonly IDvlSqlConnection _dvlSqlConnection;
        private readonly DvlSqlUpdateExpression _updateExpression;
        private IInsertDeleteExecutable<int> _updateExecutable;

        public SqlUpdateable(IDvlSqlConnection dvlSqlConnection, DvlSqlUpdateExpression updateExpression) =>
            (this._dvlSqlConnection, this._updateExpression) = (dvlSqlConnection, updateExpression);

        public IUpdateable Set<TVal>(DvlSqlType<TVal> value)
        {
            this._updateExpression.Add(value);

            return this;
        }

        public IUpdateable Set(DvlSqlParameter value)
        {
            this._updateExpression.Add(value);

            return this;
        }

        public async Task<int> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default) =>
            this._updateExecutable != null
                ? await this._updateExecutable.ExecuteAsync(timeout, cancellationToken)
                : await CreateDeleteExecutable().ExecuteAsync(timeout, cancellationToken);

        public Task<TResult> ExecuteAsync<TResult>(Func<IDataReader, TResult> reader, int? timeout = default, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IInsertDeleteExecutable<int> Where(DvlSqlBinaryExpression binaryExpression, params DvlSqlParameter[] @params)
        {
            this._updateExpression.WhereExpression = new DvlSqlWhereExpression(binaryExpression).WithParameters(@params) as DvlSqlWhereExpression;

            return this._updateExecutable = CreateDeleteExecutable();
        }

        public IInsertDeleteExecutable<int> CreateDeleteExecutable() =>
            new SqlInsertDeleteExecutable<int>(this._dvlSqlConnection, ToString, GetDvlSqlParameters,
                (command, timeout, token) => command.ExecuteNonQueryAsync(timeout, token ?? default));

        public override string ToString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this._updateExpression.Accept(commandBuilder);

            return builder.ToString();
        }

        private IEnumerable<DvlSqlParameter> GetDvlSqlParameters() =>
            this._updateExpression.DvlSqlParameters.Union(this._updateExpression.WhereExpression?.Parameters ??
                                                          Enumerable.Empty<DvlSqlParameter>());
    }
}
