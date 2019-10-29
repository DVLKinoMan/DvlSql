using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Concrete
{
    public class Whereable : IWhereable
    {
        private readonly IDvlSelectable _selectable;

        public Whereable(IDvlSelectable selectable) => this._selectable = selectable;

        public IOrderable OrderBy(params string[] fields) => this._selectable.OrderBy(fields);

        public IOrderable OrderByDescending(params string[] fields) => this._selectable.OrderByDescending(fields);

        public IExecutor Select(int? topNum = null, params string[] parameterNames) => this._selectable.Select(topNum, parameterNames);

        public IExecutor Select(int? topNum = null) => this._selectable.Select(topNum);
    }
}
