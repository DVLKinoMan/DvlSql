using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSqlCommand //: IDvlSqlCommand<TResult>
    {
        //private Func<int?, CancellationToken, Task<TResult>> _executeAsync;
        private readonly Func<SqlDataReaderType, CancellationToken, Task<SqlDataReader>> _readAsync;

        public DvlSqlCommand(Func<SqlDataReaderType, CancellationToken, Task<SqlDataReader>> readAsync) =>
            this._readAsync = readAsync;

        //public Task<TResult> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default) =>
        //    this._executeAsync(timeout, cancellationToken);

        public Task<SqlDataReader> ReadAsync(SqlDataReaderType type, CancellationToken token) =>
            this._readAsync(type, token);
    }
}
