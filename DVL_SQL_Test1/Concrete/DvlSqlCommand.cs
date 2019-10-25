using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Concrete
{
    /// <summary>
    /// todo maybe sqlcommand will have timeout and cancellationtoken already
    /// </summary>
    public class DvlSqlCommand : IDvlSqlCommand //: IDvlSqlCommand<TResult>
    {
        ////private Func<int?, CancellationToken, Task<TResult>> _executeAsync;
        //private readonly Func<SqlDataReaderType, CancellationToken, Task<SqlDataReader>> _readAsync;

        //public DvlSqlCommand(Func<SqlDataReaderType, CancellationToken, Task<SqlDataReader>> readAsync) =>
        //    this._readAsync = readAsync;

        ////public Task<TResult> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default) =>
        ////    this._executeAsync(timeout, cancellationToken);

        //public Task<SqlDataReader> ReadAsync(SqlDataReaderType type, CancellationToken token) =>
        //    this._readAsync(type, token);
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

            var reader = await this._sqlCommand.ExecuteReaderAsync(behavior, cancellationToken);

            return converterFunc(reader);
        }

        public async Task<TResult> ExecuteScalarAsync<TResult>(Func<object, TResult> converterFunc, int? timeout = default, CancellationToken cancellationToken = default)
        {
            if (timeout != null)
                WithTimeout(this._sqlCommand, (int)timeout);

            var result = await this._sqlCommand.ExecuteScalarAsync(cancellationToken);

            return converterFunc(result);
        }
    }
}
