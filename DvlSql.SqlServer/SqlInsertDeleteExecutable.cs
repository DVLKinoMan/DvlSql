using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DvlSql.SqlServer
{
    internal class SqlInsertDeleteExecutable<TResult> : IInsertDeleteExecutable<TResult>
    {
        private readonly IDvlSqlConnection _connection;
        private readonly Func<string> _sqlStringFunc;
        private readonly Func<IEnumerable<DvlSqlParameter>> _getDvlSqlParameters;
        private readonly Func<IDvlSqlCommand, int?, CancellationToken?, Task<TResult>> _reader;

        public SqlInsertDeleteExecutable(IDvlSqlConnection connection, 
            Func<string> sqlStringFunc, 
            Func<IEnumerable<DvlSqlParameter>> getDvlSqlParameters, 
            Func<IDvlSqlCommand, int?, CancellationToken?, Task<TResult>> reader)
        {
            _reader = reader;
            (this._connection, this._sqlStringFunc, this._getDvlSqlParameters) =
                (connection, sqlStringFunc, getDvlSqlParameters);
        }

        public async Task<TResult> ExecuteAsync(int? timeout = default, 
            CancellationToken cancellationToken = default) =>
            await this._connection.ConnectAsync(
                dvlCommand => _reader(dvlCommand, timeout, cancellationToken), 
                this._sqlStringFunc(),
                parameters: this._getDvlSqlParameters()?.ToArray());

        public override string ToString() => this._sqlStringFunc();
    }
}
