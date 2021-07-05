using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace DvlSql.Abstract
{
    //public interface IInsertDeleteExecutable
    //{
    //    IInsertDeleteExecutable<TResult> Output<TResult>(Func<IDataReader, TResult> reader, params string[] cols);
    //    Task<int> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default);

    //    Task<TResult> ExecuteAsync<TResult>(Func<IDataReader, TResult> reader, int? timeout = default,
    //        CancellationToken cancellationToken = default);
    //}

    public interface IInsertDeleteExecutable<TResult>
    {
        Task<TResult> ExecuteAsync(int? timeout = default,
            CancellationToken cancellationToken = default);
    }
}
