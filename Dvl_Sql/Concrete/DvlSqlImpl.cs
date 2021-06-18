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
        private readonly IDvlSqlConnection _dvlSqlConnection;

        public DvlSqlImpl(string connectionString) => 
            this._dvlSqlConnection = new DvlSqlConnection(connectionString);

        public DvlSqlImpl(IDvlSqlConnection connection) =>
            this._dvlSqlConnection = connection;
        
        public ISelector From(string tableName, bool withNoLock = false)
        {
            var fromExpression = new DvlSqlFromExpression(tableName, withNoLock);

            return new SqlSelector(fromExpression, this._dvlSqlConnection);
        }

        public ISelector From(DvlSqlFullSelectExpression @select, string @as)
        {
            var fromExpression = new DvlSqlFromExpression(@select, @as);

            return new SqlSelector(fromExpression, this._dvlSqlConnection);
        }

        public IDeletable DeleteFrom(string tableName)
        {
            var fromExpression = new DvlSqlFromExpression(tableName);

            return new SqlDeletable(fromExpression, this._dvlSqlConnection);
        }

        public IUpdateSetable Update(string tableName)
        {
            var updateExpression = new DvlSqlUpdateExpression(tableName);

            return new SqlUpdateable(this._dvlSqlConnection, updateExpression);
        }

        public IProcedureExecutable Procedure(string procedureName, params DvlSqlParameter[] parameters) =>
            new SqlProcedureExecutable(this._dvlSqlConnection, procedureName, parameters);

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

        public async Task BeginTransactionAsync(CancellationToken token = default) => 
            await this._dvlSqlConnection.BeginTransactionAsync(token);
    }
}