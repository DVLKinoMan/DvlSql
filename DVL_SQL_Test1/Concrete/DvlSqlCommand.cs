using DVL_SQL_Test1.Abstract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSqlCommand<TResult> : IDvlSqlCommand<TResult>
    {
        private Func<int?, CancellationToken, Task<TResult>> _executeAsync;

        public DvlSqlCommand(Func<int?, CancellationToken, Task<TResult>> executeAsync) =>
            this._executeAsync = executeAsync;

        public Task<TResult> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default) =>
            this._executeAsync(timeout, cancellationToken);
    }
}
