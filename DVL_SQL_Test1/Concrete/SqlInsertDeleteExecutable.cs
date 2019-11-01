using DVL_SQL_Test1.Abstract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlInsertDeleteExecutable : IInsertDeleteExecutable
    {
        private readonly IDvlSqlConnection _connection;
        private readonly Func<string> _sqlStringFunc;

        public SqlInsertDeleteExecutable(IDvlSqlConnection connection, Func<string> sqlStringFunc) =>
            (this._connection, this._sqlStringFunc) = (connection, sqlStringFunc);

        public async Task<int> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default) =>
            await this._connection.ConnectAsync(
                dvlCommand => dvlCommand.ExecuteNonQueryAsync(timeout, cancellationToken), this._sqlStringFunc());
    }
}
