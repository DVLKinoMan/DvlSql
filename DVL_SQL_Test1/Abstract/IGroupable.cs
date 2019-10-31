namespace DVL_SQL_Test1.Abstract
{
    // ReSharper disable once IdentifierTypo
    public interface IGroupable
    {
        IGrouper GroupBy(params string[] parameterNames);
    }
}
