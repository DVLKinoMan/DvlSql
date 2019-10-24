using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSqlConnection : IDisposable
    {
        private readonly SqlConnection _sqlConnection;
        private readonly List<SqlCommand> commands = new List<SqlCommand>();

        public DvlSqlConnection(SqlConnection connection) => this._sqlConnection = connection;

        public void Dispose()
        {
            this._sqlConnection?.Dispose();
            this.commands.Clear();
        }

        private SqlCommand CreateCommand(string sql)
        {
            var com = new SqlCommand(sql, this._sqlConnection);
            this.commands.Add(com);
            return com;
        }

        public async Task<SqlDataReader> GetExecuteReaderAsync(SqlDataReaderType type, CancellationToken token,
            string sql) =>
            type switch
            {
                SqlDataReaderType.ExecuteReaderAsync => await CreateCommand(sql).ExecuteReaderAsync(token),
                _ => throw new NotImplementedException()
            };
    }
}
