using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dvl_Sql.Abstract;
using Dvl_Sql.Models;

namespace Dvl_Sql.Concrete
{
    public class SqlProcedureExecutable : IProcedureExecutable
    {
        private readonly IDvlSqlConnection _dvlSqlConnection;
        private readonly string _procedureName;
        private readonly DvlSqlParameter[] _parameters;

        public SqlProcedureExecutable(IDvlSqlConnection dvlSqlConnection, string procedureName,
            params DvlSqlParameter[] parameters) =>
            (this._dvlSqlConnection, this._procedureName, this._parameters) =
            (dvlSqlConnection, procedureName, parameters);

        public async Task<int> ExecuteAsync(int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default,
            CancellationToken cancellationToken = default) =>
            await this._dvlSqlConnection.ConnectAsync(
                dvlCommand => dvlCommand.ExecuteNonQueryAsync(timeout, cancellationToken),
                this._procedureName,
                CommandType.StoredProcedure,
                parameters: this._parameters.Select(param => param.SqlParameter).ToArray());

        public async Task<TResult> ExecuteAsync<TResult>(Func<SqlDataReader, TResult> reader,
            int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default) =>
            await this._dvlSqlConnection.ConnectAsync(
                dvlCommand => dvlCommand.ExecuteReaderAsync(reader, timeout, behavior, cancellationToken),
                this._procedureName,
                CommandType.StoredProcedure,
                parameters: this._parameters.Select(param => param.SqlParameter).ToArray());

    }
}
