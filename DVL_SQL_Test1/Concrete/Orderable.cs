using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Concrete
{
    public class Orderable : IOrderable
    {
        private readonly IDvlSelectable _selectable;

        public Orderable(IDvlSelectable selectable) => this._selectable = selectable;

        public IExecutor Select(int? topNum = null, params string[] parameterNames) => this._selectable.Select(topNum, parameterNames);

        public IExecutor Select(int? topNum = null) => this._selectable.Select(topNum);

        public IOrderable OrderBy(params string[] fields) => this._selectable.OrderBy(fields);

        public IOrderable OrderByDescending(params string[] fields) => this._selectable.OrderByDescending(fields);
    }
}
