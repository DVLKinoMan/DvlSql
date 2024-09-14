namespace DvlSql;

public interface IUnionable
{
    IFromable Union();
    IFromable UnionAll();
}