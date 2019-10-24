using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Concrete
{
    public enum SqlDataReaderType
    {
        ExecuteReaderAsync,
        ExecuteNonQueryAsync,
        ExecuteScalarAsync
    }

    public class DvlSql : IDvlFrom
    {
        private readonly string _connectionString;

        public DvlSql(string connectionString) => this._connectionString = connectionString;

        public IDvlSelectable From(string tableName)
        {
            var fromExpression = new DvlSqlFromExpression(tableName);

            return new DvlSqlSelectable(fromExpression, CreateCommand);
        }

        private DvlSqlCommand CreateCommand(string sql) => new DvlSqlCommand((type, token) => CreateConnectionAsync(type, token,sql));

        private async Task<SqlDataReader> CreateConnectionAsync(SqlDataReaderType type, CancellationToken token,
            string sql)
        {
            await using var connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync(token);
            using var dvlConnection = new DvlSqlConnection(connection);
            return await dvlConnection.GetExecuteReaderAsync(type, token, sql);
        }

    }
}
