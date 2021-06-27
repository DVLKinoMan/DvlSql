﻿using System.Threading;
using System.Threading.Tasks;

namespace DvlSql.Abstract
{
    public interface ITransaction
    {
        Task<IDvlSqlConnection> BeginTransactionAsync(CancellationToken token = default);
        Task CommitAsync(CancellationToken token = default);
        Task RollbackAsync(CancellationToken token = default);
    }
}
