using DvlSql.Abstract;
using DvlSql.Expressions;
using DvlSql.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DvlSql.Concrete
{
    internal partial class DvlSqlImpl : IDvlSql
    {
        private IDvlSqlConnection _dvlSqlConnection;
        private readonly string _connectionString;

        public DvlSqlImpl(string connectionString) => this._connectionString = connectionString;

        public DvlSqlImpl(IDvlSqlConnection connection) => this._dvlSqlConnection = connection;

        private IDvlSqlConnection GetConnection() => this._dvlSqlConnection ?? new DvlSqlConnection(_connectionString);
        
        public ISelector From(string tableName, bool withNoLock = false)
        {
            var fromExpression = new DvlSqlFromExpression(tableName, withNoLock);

            return new SqlSelector(fromExpression, GetConnection());
        }

        public ISelector From(DvlSqlFullSelectExpression @select, string @as)
        {
            var fromExpression = new DvlSqlFromExpression(@select, @as);

            return new SqlSelector(fromExpression, GetConnection());
        }

        public IDeletable DeleteFrom(string tableName)
        {
            var fromExpression = new DvlSqlFromExpression(tableName);

            return new SqlDeletable(fromExpression, GetConnection());
        }

        public IUpdateSetable Update(string tableName)
        {
            var updateExpression = new DvlSqlUpdateExpression(tableName);

            return new SqlUpdateable(GetConnection(), updateExpression);
        }

        public IDvlSql SetConnection(IDvlSqlConnection connection)
        {
            this._dvlSqlConnection = connection;

            return this;
        }

        public IProcedureExecutable Procedure(string procedureName, params DvlSqlParameter[] parameters) =>
            new SqlProcedureExecutable(GetConnection(), procedureName, parameters);

        public async Task CommitAsync(CancellationToken token = default)
        {
            try
            {
                await this._dvlSqlConnection.CommitAsync(token);
            }
            catch (Exception exc)
            {
                var list = new List<Exception> {exc};
                try
                {
                    await this._dvlSqlConnection.RollbackAsync(token);
                }
                catch (Exception exc2)
                {
                    list.Add(exc2);
                }

                throw new AggregateException(list);
            }
            finally
            {
                this._dvlSqlConnection.Dispose();
            }
        }

        public async Task RollbackAsync(CancellationToken token = default) =>
            await this._dvlSqlConnection.RollbackAsync(token);

        public async Task<IDvlSqlConnection> BeginTransactionAsync(CancellationToken token = default)
        {
            var conn = GetConnection().GetClone();
            await conn.BeginTransactionAsync(token);
            return conn;
        }
    }
}