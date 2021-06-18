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
        //private readonly IDvlSqlConnection _dvlSqlConnection;
        private readonly string _connectionString;
        private Dictionary<int, IDvlSqlConnection> _connections = new Dictionary<int, IDvlSqlConnection>();

        public DvlSqlImpl(string connectionString) =>
            this._connectionString = connectionString;
            //this._dvlSqlConnection = new DvlSqlConnection(connectionString);

        public DvlSqlImpl(IDvlSqlConnection connection)
        {
            //this._dvlSqlConnection = connection;
        }

        private IDvlSqlConnection GetConnection() => _connections.ContainsKey(Thread.CurrentThread.ManagedThreadId) 
            ? _connections[Thread.CurrentThread.ManagedThreadId]  
            : new DvlSqlConnection(this._connectionString);
        
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

        public IProcedureExecutable Procedure(string procedureName, params DvlSqlParameter[] parameters) =>
            new SqlProcedureExecutable(GetConnection(), procedureName, parameters);

        public async Task CommitAsync(CancellationToken token = default)
        {
            try
            {
                await _connections[Thread.CurrentThread.ManagedThreadId].CommitAsync(token);
            }
            catch (Exception exc)
            {
                var list = new List<Exception> {exc};
                try
                {
                    await _connections[Thread.CurrentThread.ManagedThreadId].RollbackAsync(token);
                }
                catch (Exception exc2)
                {
                    list.Add(exc2);
                }

                throw new AggregateException(list);
            }
            finally
            {
                _connections[Thread.CurrentThread.ManagedThreadId].Dispose();
                _connections.Remove(Thread.CurrentThread.ManagedThreadId);
            }
        }

        public async Task RollbackAsync(CancellationToken token = default) =>
            await _connections[Thread.CurrentThread.ManagedThreadId].RollbackAsync(token);

        public async Task BeginTransactionAsync(CancellationToken token = default)
        {
            var conn = GetConnection();
            await conn.BeginTransactionAsync(token);
            _connections[Thread.CurrentThread.ManagedThreadId] = conn;
        }
    }
}