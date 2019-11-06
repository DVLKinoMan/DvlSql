using System.Collections.Generic;
using System.Linq;
using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DVL_SQL_Test1.Models;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlUpdateable : IUpdateable
    {
        private readonly string _connectionString;
        private readonly IUpdateSetable _updateSetable;
        private readonly DvlSqlUpdateExpression _updateExpression;
        private IInsertDeleteExecutable _updateExecutable;

        public SqlUpdateable(string connectionString, DvlSqlUpdateExpression updateExpression, IUpdateSetable updateSetable) =>
            (this._connectionString, this._updateExpression, this._updateSetable) = (connectionString, updateExpression,  updateSetable);

        public IUpdateable Set<TVal>((string, DvlSqlType<TVal>) value) => this._updateSetable.Set(value);

        public async Task<int> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default) =>
            this._updateExecutable != null
                ? await this._updateExecutable.ExecuteAsync(timeout, cancellationToken)
                : await CreateDeleteExecutable().ExecuteAsync(timeout, cancellationToken);

        public IInsertDeleteExecutable Where(DvlSqlBinaryExpression binaryExpression, params DvlSqlParameter[] @params)
        {
            this._updateExpression.WhereExpression = new DvlSqlWhereExpression(binaryExpression).WithParameters(@params) as DvlSqlWhereExpression;

            return this._updateExecutable = CreateDeleteExecutable();
        }

        public IInsertDeleteExecutable CreateDeleteExecutable() =>
            new SqlInsertDeleteExecutable(new DvlSqlConnection(this._connectionString), GetSqlString, GetDvlSqlParameters);

        private string GetSqlString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this._updateExpression.Accept(commandBuilder);

            return builder.ToString();
        }

        private IEnumerable<DvlSqlParameter> GetDvlSqlParameters() => this._updateExpression.Parameters.Union(this._updateExpression.WhereExpression?.Parameters);
    }
}
