using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlGroupBy : IDvlGroupBy
    {
        private readonly IDvlSelect _select;

        public DvlGroupBy(IDvlSelect select) => this._select = select;

        public IDvlSqlExecutor Select(params string[] parameterNames) => this._select.Select(parameterNames);

        public IDvlSqlExecutor Select() => this._select.Select();

        public IDvlSqlExecutor SelectTop(int count, params string[] parameterNames) =>
            this._select.SelectTop(count, parameterNames);

        public IDvlOrderBy OrderBy(params string[] fields) => this._select.OrderBy(fields);

        public IDvlOrderBy OrderByDescending(params string[] fields) => this._select.OrderByDescending(fields);
    }
}
