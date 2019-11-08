using DVL_SQL_Test1.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Abstract
{
    public interface IProcedureExecutable
    {
        Task<int> ExecuteProcedureAsync(string procedureName, int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default,
            params DvlSqlParameter[] parameters);

        Task<TResult> ExecuteProcedureAsync<TResult>(string procedureName, Func<SqlDataReader, TResult> reader,
            int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default,
            params DvlSqlParameter[] parameters);
    }
}
