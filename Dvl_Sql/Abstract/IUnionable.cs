namespace DvlSql.Abstract
{
    public interface IUnionable
    {
        IFromable Union();
        IFromable UnionAll();
    }
}