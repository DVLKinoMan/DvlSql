using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlGroupBy : IDvlGroupBy
    {
        private readonly IDvlSelect _select;

        public DvlGroupBy(IDvlSelect select) => this._select = select;

        public IDvlOrderBy Select(params string[] parameterNames) => this._select.Select(parameterNames);

        public IDvlOrderBy Select() => this._select.Select();

        public IDvlOrderBy SelectTop(int count, params string[] parameterNames) =>
            this._select.SelectTop(count, parameterNames);

    }
}
