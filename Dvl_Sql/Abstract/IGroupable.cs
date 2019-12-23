namespace Dvl_Sql.Abstract
{
    // ReSharper disable once IdentifierTypo
    public interface IGroupable
    {
        IGrouper GroupBy(params string[] parameterNames);
    }
}
