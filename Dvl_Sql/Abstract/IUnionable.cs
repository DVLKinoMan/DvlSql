namespace Dvl_Sql.Abstract
{
    public interface IUnionable
    {
        IFromable Union();
        IFromable UnionAll();
    }
}