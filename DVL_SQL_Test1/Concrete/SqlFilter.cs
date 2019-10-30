using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlFilter : IFilter
    {
        private readonly ISelector _selector;

        public SqlFilter(ISelector selector) => this._selector = selector;

        public IGrouper GroupBy(params string[] parameterNames) => this._selector.GroupBy(parameterNames);

        public IExecutor Select(params string[] parameterNames) => this._selector.Select(parameterNames);

        public IExecutor Select() => this._selector.Select();

        public IExecutor SelectTop(int count, params string[] parameterNames) =>
            this._selector.SelectTop(count, parameterNames);
    }
}
