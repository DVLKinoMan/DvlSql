using Dvl_Sql.Abstract;

namespace Dvl_Sql.Concrete
{
    public class SqlFilter : IFilter
    {
        private readonly ISelector _selector;

        public SqlFilter(ISelector selector) => this._selector = selector;

        public IGrouper GroupBy(params string[] parameterNames) => this._selector.GroupBy(parameterNames);

        public IOrderer Select(params string[] parameterNames) => this._selector.Select(parameterNames);

        public IOrderer Select() => this._selector.Select();

        public IOrderer SelectTop(int count, params string[] parameterNames) =>
            this._selector.SelectTop(count, parameterNames);
    }
}
