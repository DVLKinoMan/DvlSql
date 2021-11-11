namespace DvlSql
{
    public interface ISelectable
    {
        IOrderer Select(params string[] parameterNames);
        IOrderer Select();
        IOrderer SelectTop(int count, params string[] parameterNames);
    }
}
