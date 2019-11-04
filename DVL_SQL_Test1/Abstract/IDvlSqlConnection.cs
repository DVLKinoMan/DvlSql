﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlSqlConnection
    {
        Task<TResult> ConnectAsync<TResult>(Func<IDvlSqlCommand, Task<TResult>> func, string sqlString, CommandType commandType = CommandType.Text, params SqlParameter[] parameters);
    }
}
