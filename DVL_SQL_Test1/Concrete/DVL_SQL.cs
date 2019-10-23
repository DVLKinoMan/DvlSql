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

    public class DVL_SQL : IDvlFrom
    {
        private string _connectionString;

        public DVL_SQL(string connectionString) => this._connectionString = connectionString;

        public IDvlSelectable From(string tableName)
        {
            var fromExpression = new DvlSqlFromExpression(tableName);

            return new DvlSqlSelectable(fromExpression, GetFunc);
        }

        private async Task<Func<SqlDataReaderType, CancellationToken, Task<SqlDataReader>>> GetFunc(string str)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(str, connection);
                return async (type, token) =>
                {
                    return type switch
                    {
                        SqlDataReaderType.ExecuteReaderAsync => await command.ExecuteReaderAsync(token),
                        _ => throw new NotImplementedException()
                    };
                };
            }
        }

    }
}
