using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Concrete
{
    public class Whereable : IWhereable
    {
        private readonly IDvlSelectable _selectable;

        public Whereable(IDvlSelectable selectable) => this._selectable = selectable;

        public IOrderable OrderBy(params string[] fields) => this._selectable.OrderBy(fields);

        public IOrderable OrderByDescending(params string[] fields) => this._selectable.OrderByDescending(fields);

        public IExecutor Select(params string[] parameterNames) => this._selectable.Select(parameterNames);

        public IExecutor Select() => this._selectable.Select();

        public IExecutor SelectTop(int count, params string[] parameterNames) =>
            this._selectable.SelectTop(count, parameterNames);
    }
}
