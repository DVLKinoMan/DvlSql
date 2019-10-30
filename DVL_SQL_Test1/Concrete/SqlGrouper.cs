using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlGrouper : IGrouper
    {
        private readonly ISelector _selector;

        public SqlGrouper(ISelector selector) => this._selector = selector;

        public IOrderer Select(params string[] parameterNames) => this._selector.Select(parameterNames);

        public IOrderer Select() => this._selector.Select();

        public IOrderer SelectTop(int count, params string[] parameterNames) =>
            this._selector.SelectTop(count, parameterNames);

    }
}
