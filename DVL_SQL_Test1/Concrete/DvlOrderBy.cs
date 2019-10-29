using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlOrderBy : IDvlOrderBy
    {
        private readonly IDvlSelect _selectable;

        public DvlOrderBy(IDvlSelect selectable) => this._selectable = selectable;

        public IDvlSqlExecutor Select(params string[] parameterNames) => this._selectable.Select(parameterNames);

        public IDvlSqlExecutor Select() => this._selectable.Select();

        public IDvlSqlExecutor SelectTop(int count, params string[] parameterNames) =>
            this._selectable.SelectTop(count, parameterNames);

        public IDvlOrderBy OrderBy(params string[] fields) => this._selectable.OrderBy(fields);

        public IDvlOrderBy OrderByDescending(params string[] fields) => this._selectable.OrderByDescending(fields);
    }
}
