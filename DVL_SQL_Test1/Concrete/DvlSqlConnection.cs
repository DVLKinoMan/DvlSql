﻿using DVL_SQL_Test1.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSqlConnection : IDvlSqlConnection, IDisposable
    {
        private readonly List<SqlCommand> _commands = new List<SqlCommand>();
        private readonly string _connectionString;

        public DvlSqlConnection(string connectionString) =>
            this._connectionString = connectionString;

        public void Dispose() => this._commands.Clear();

        private DvlSqlCommand CreateCommand(CommandType commandType, SqlConnection connection, string sqlString, params SqlParameter[] parameters)
        {
            var command = new SqlCommand(sqlString, connection)
            {
                CommandType = commandType
            };

            foreach (var parameter in parameters)
                command.Parameters.Add(parameter);

            this._commands.Add(command);

            return new DvlSqlCommand(command);
        }

        public async Task<TResult> ConnectAsync<TResult>(Func<IDvlSqlCommand, Task<TResult>> func, string sqlString, CommandType commandType = CommandType.Text, params SqlParameter[] parameters )
        {
            await using var connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            return await func(CreateCommand(commandType, connection, sqlString, parameters));
        }
    }
}
