using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlSqlCommand
    {
        Task<int> ExecuteNonQueryAsync(int? timeout = default, CancellationToken cancellationToken = default);

        Task<TResult> ExecuteReaderAsync<TResult>(Func<SqlDataReader, TResult> converterFunc, int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default);

        Task<TResult> ExecuteScalarAsync<TResult>(Func<object, TResult> converterFunc, int? timeout = default, CancellationToken cancellationToken = default);
    }
}
