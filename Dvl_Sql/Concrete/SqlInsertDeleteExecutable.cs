using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DvlSql.Abstract;
using DvlSql.Models;

namespace DvlSql.Concrete
{
    internal class SqlInsertDeleteExecutable : IInsertDeleteExecutable
    {
        private readonly IDvlSqlConnection _connection;
        private readonly Func<string> _sqlStringFunc;
        private readonly Func<IEnumerable<DvlSqlParameter>> _getDvlSqlParameters;

        public SqlInsertDeleteExecutable(IDvlSqlConnection connection, Func<string> sqlStringFunc, Func<IEnumerable<DvlSqlParameter>> getDvlSqlParameters) =>
            (this._connection, this._sqlStringFunc, this._getDvlSqlParameters) = (connection, sqlStringFunc, getDvlSqlParameters);

        public async Task<int> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default) =>
            await this._connection.ConnectAsync(
                dvlCommand => dvlCommand.ExecuteNonQueryAsync(timeout, cancellationToken), this._sqlStringFunc(),
                parameters: this._getDvlSqlParameters()?.Select(param => param.SqlParameter).ToArray());

        public override string ToString() => this._sqlStringFunc();
    }
}
