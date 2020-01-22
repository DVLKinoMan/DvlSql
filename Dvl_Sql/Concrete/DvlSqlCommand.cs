using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dvl_Sql.Abstract;

namespace Dvl_Sql.Concrete
{
    /// <summary>
    /// todo maybe sqlcommand will have timeout and cancellationtoken already
    /// </summary>
    internal class DvlSqlCommand : IDvlSqlCommand //: IDvlSqlCommand<TResult>
    {
        private readonly SqlCommand _sqlCommand;

        public DvlSqlCommand(SqlCommand command) => this._sqlCommand = command;

        private static SqlCommand WithTimeout(SqlCommand command, int timeout)
        {
            command.CommandTimeout = timeout;
            return command;
        }

        public async Task<int> ExecuteNonQueryAsync(int? timeout = default, CancellationToken cancellationToken = default)
        {
            if (timeout != null)
                WithTimeout(this._sqlCommand, (int) timeout);

            return await this._sqlCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        public async Task<TResult> ExecuteReaderAsync<TResult>(Func<SqlDataReader, TResult> converterFunc,
            int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default)
        {
            if (timeout != null)
                WithTimeout(this._sqlCommand, (int) timeout);

            await using var reader = await this._sqlCommand.ExecuteReaderAsync(behavior, cancellationToken);

            return converterFunc(reader);
        }

        public async Task<TResult> ExecuteScalarAsync<TResult>(Func<object, TResult> converterFunc, 
            int? timeout = default, CancellationToken cancellationToken = default)
        {
            if (timeout != null)
                WithTimeout(this._sqlCommand, (int)timeout);

            var result = await this._sqlCommand.ExecuteScalarAsync(cancellationToken);

            return converterFunc(result);
        }

        public void Dispose()
        {
            _sqlCommand?.Dispose();
            this._sqlCommand?.Parameters.Clear();
        }
    }
}
