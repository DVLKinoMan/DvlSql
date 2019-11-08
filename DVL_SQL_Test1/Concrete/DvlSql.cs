using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DVL_SQL_Test1.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSql : IDvlSql
    {
        private readonly IDvlSqlConnection _dvlSqlConnection;

        public DvlSql(string connectionString) => this._dvlSqlConnection = new DvlSqlConnection(connectionString);

        public ISelector From(string tableName, bool withNoLock = false)
        {
            var fromExpression = new DvlSqlFromExpression(tableName, withNoLock);

            return new SqlSelector(fromExpression, this._dvlSqlConnection);
        }

        public IInsertable<TRes> InsertInto<TRes>(string tableName, params (string col, DvlSqlType sqlType)[] types)
            where TRes : ITuple
        {
            var insertExpression = new DvlSqlInsertIntoExpression<TRes>(tableName, types);

            return new SqlInsertable<TRes>(insertExpression, this._dvlSqlConnection);
        }

        public IInsertable InsertInto(string tableName, IEnumerable<string> cols)
        {
            var insertExpression = new DvlSqlInsertIntoSelectExpression(tableName, cols.ToArray());

            return new SqlInsertable(insertExpression, this._dvlSqlConnection);
        }

        public IDeletable DeleteFrom(string tableName)
        {
            var fromExpression = new DvlSqlFromExpression(tableName);

            return new SqlDeletable(fromExpression, this._dvlSqlConnection);
        }

        public IUpdateSetable Update(string tableName)
        {
            var updateExpression = new DvlSqlUpdateExpression(tableName);

            return new SqlUpdateSetable(this._dvlSqlConnection, updateExpression);
        }

        public async Task<int> ExecuteProcedureAsync(string procedureName, int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default,
            params DvlSqlParameter[] parameters) =>
            await this._dvlSqlConnection.ConnectAsync(
                dvlCommand => dvlCommand.ExecuteNonQueryAsync(timeout, cancellationToken),
                procedureName,
                CommandType.StoredProcedure,
                parameters: parameters.Select(param => param.SqlParameter).ToArray());

        public async Task<TResult> ExecuteProcedureAsync<TResult>(string procedureName,
            Func<SqlDataReader, TResult> reader, int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default,
            params DvlSqlParameter[] parameters) =>
            await this._dvlSqlConnection.ConnectAsync(
                dvlCommand => dvlCommand.ExecuteReaderAsync(reader, timeout, behavior, cancellationToken),
                procedureName,
                CommandType.StoredProcedure,
                parameters: parameters.Select(param => param.SqlParameter).ToArray());
    }
}
