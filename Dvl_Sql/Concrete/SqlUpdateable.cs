using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dvl_Sql.Abstract;
using Dvl_Sql.Expressions;
using Dvl_Sql.Models;

namespace Dvl_Sql.Concrete
{
    public class SqlUpdateable : IUpdateable
    {
        private readonly IDvlSqlConnection _dvlSqlConnection;
        private readonly IUpdateSetable _updateSetable;
        private readonly DvlSqlUpdateExpression _updateExpression;
        private IInsertDeleteExecutable _updateExecutable;

        public SqlUpdateable(IDvlSqlConnection dvlSqlConnection, DvlSqlUpdateExpression updateExpression, IUpdateSetable updateSetable) =>
            (this._dvlSqlConnection, this._updateExpression, this._updateSetable) = (dvlSqlConnection, updateExpression,  updateSetable);

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
            new SqlInsertDeleteExecutable(this._dvlSqlConnection, GetSqlString, GetDvlSqlParameters);

        private string GetSqlString()
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
