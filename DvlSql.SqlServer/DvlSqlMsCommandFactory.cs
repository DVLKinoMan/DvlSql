﻿using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DvlSql.SqlServer
{
    internal class DvlSqlMsCommandFactory : IDvlSqlMsCommandFactory
    {
        public IDvlSqlCommand CreateSqlCommand(CommandType commandType, SqlConnection connection, string sqlString, DbTransaction transaction = null,
            params SqlParameter[] parameters)
        {
            var command = new SqlCommand(sqlString, connection)
            {
                CommandType = commandType
            };

            if (transaction != null)
                command.Transaction = (SqlTransaction) transaction;

            if(parameters!=null)
                foreach (var parameter in parameters)
                    command.Parameters.Add(parameter);

            return new DvlSqlCommand(command);
        }
    }
}