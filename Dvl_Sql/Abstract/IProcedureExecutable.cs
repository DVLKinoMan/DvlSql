﻿using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Dvl_Sql.Abstract
{
    public interface IProcedureExecutable
    {
        Task<int> ExecuteAsync(int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default);

        Task<TResult> ExecuteAsync<TResult>(Func<IDataReader, TResult> reader,
            int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default);
    }
}
