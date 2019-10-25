﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Abstract
{
    public interface IExecutor
    {
        (int, bool) Execute();

        Task<List<TResult>> ToListAsync<TResult>(Func<SqlDataReader, TResult> reader);

        Task<List<TResult>> ToListAsync<TResult>(int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default);

        TResult FirstOrDefault<TResult>();
    }
}
