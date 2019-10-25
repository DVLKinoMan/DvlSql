using DVL_SQL_Test1.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSqlConnection : IDvlSqlConnection, IDisposable
    {
        private readonly List<SqlCommand> _commands = new List<SqlCommand>();
        private readonly string _connectionString;
        private readonly string _sqlString;

        public DvlSqlConnection(string connectionString, string sqlString) =>
            (this._connectionString, this._sqlString) = (connectionString, sqlString);

        public void Dispose()
        {
            this._commands.Clear();
        }

        private DvlSqlCommand CreateCommand(SqlConnection connection)
        {
            var command = new SqlCommand(this._sqlString, connection);
            this._commands.Add(command);
            return new DvlSqlCommand(command);
        }

        public async Task<TResult> ConnectAsync<TResult>(Func<IDvlSqlCommand, Task<TResult>> func)
        {
            await using var connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            return await func(CreateCommand(connection));
        }
    }
}
