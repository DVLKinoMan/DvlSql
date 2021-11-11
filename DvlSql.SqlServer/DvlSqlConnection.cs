﻿using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DvlSql.SqlServer
{
    internal class DvlSqlConnection : IDvlSqlConnection
    {
        // private readonly List<SqlCommand> _commands = new List<SqlCommand>();
        private readonly SqlConnection _connection;
        private DbTransaction _transaction;
        private readonly IDvlSqlMsCommandFactory _commandFactory;

        public DvlSqlConnection(string connectionString) =>
            (this._connection, this._commandFactory) = (new SqlConnection(connectionString), new DvlSqlMsCommandFactory());
        
        public DvlSqlConnection(string connectionString, IDvlSqlMsCommandFactory commandFactory) =>
            (this._connection, this._commandFactory) = (new SqlConnection(connectionString), commandFactory);

        public void Dispose()
        {
            //this._commands.Clear();
            this._transaction.Dispose();
            this._transaction = null;
            if(this._connection.State != ConnectionState.Closed)
                this._connection.Close();
        }

        public async Task<TResult> ConnectAsync<TResult>(Func<IDvlSqlCommand, Task<TResult>> func, string sqlString,
            CommandType commandType = CommandType.Text, params DvlSqlParameter[] parameters)
        {
            if (this._transaction == null)
                await this._connection.OpenAsync();
            using var command =
                this._commandFactory.CreateSqlCommand(commandType, this._connection, sqlString, this._transaction, 
                parameters.ToSqlParameters().ToArray());
            try
            {
                return await func(command);
            }
            finally
            {
                if (this._transaction == null)
                    await this._connection.CloseAsync();
            }
        }

        public async ValueTask<DbTransaction> BeginTransactionAsync(CancellationToken token = default)
        {
            await this._connection.OpenAsync(token);
            return this._transaction = await this._connection.BeginTransactionAsync(token);
        }

        public async Task CommitAsync(CancellationToken token = default)
        {
            if (this._transaction == null)
                throw new ArgumentNullException(nameof(_transaction));
         
            await this._transaction.CommitAsync(token);
        }

        public async Task RollbackAsync(CancellationToken token = default) 
        {
            if (this._transaction == null)
                throw new ArgumentNullException(nameof(_transaction));

            await this._transaction.RollbackAsync(token);
        }

        public IDvlSqlConnection GetClone() =>
            new DvlSqlConnection(this._connection.ConnectionString, this._commandFactory);

        public async ValueTask DisposeAsync()
        {
            //this._commands.Clear();
            await this._transaction.DisposeAsync();
            this._transaction = null;
            if (this._connection.State != ConnectionState.Closed)
                await this._connection.CloseAsync();
        }
    }
}
