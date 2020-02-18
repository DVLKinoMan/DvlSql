using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dvl_Sql.Abstract;

namespace Dvl_Sql.Concrete
{
    internal class DvlSqlConnection : IDvlSqlConnection
    {
        // private readonly List<SqlCommand> _commands = new List<SqlCommand>();
        private readonly string _connectionString;
        private readonly IDvlMsSqlCommandFactory _commandFactory;

        public DvlSqlConnection(string connectionString) =>
            (this._connectionString, this._commandFactory) = (connectionString, new DvlMSSqlCommandFactory());
        
        public DvlSqlConnection(string connectionString, IDvlMsSqlCommandFactory commandFactory) =>
            (this._connectionString, this._commandFactory) = (connectionString, commandFactory);

        public void Dispose()
        {
            //this._commands.Clear();
        }

        // private DvlSqlCommand CreateCommand(CommandType commandType, SqlConnection connection, 
        //     string sqlString, params SqlParameter[] parameters)
        // {
        //     var command = new SqlCommand(sqlString, connection)
        //     {
        //         CommandType = commandType
        //     };
        //
        //     if(parameters!=null)
        //         foreach (var parameter in parameters)
        //             command.Parameters.Add(parameter);
        //
        //     this._commands.Add(command);
        //
        //     return new DvlSqlCommand(command);
        // }

        public async Task<TResult> ConnectAsync<TResult>(Func<IDvlSqlCommand, Task<TResult>> func, string sqlString, 
            CommandType commandType = CommandType.Text, params SqlParameter[] parameters )
        {
            await using var connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            using var command = this._commandFactory.CreateSqlCommand(commandType, connection, sqlString, parameters);
            return await func(command);
        }
    }
}
