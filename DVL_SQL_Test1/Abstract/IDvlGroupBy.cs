namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlGroupBy
    {
        IDvlOrderBy Select(params string[] parameterNames);
        IDvlOrderBy Select();
        IDvlOrderBy SelectTop(int count, params string[] parameterNames);
    }
}
