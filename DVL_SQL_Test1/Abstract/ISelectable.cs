namespace Dvl_Sql.Abstract
{
    public interface ISelectable
    {
        IOrderer Select(params string[] parameterNames);
        IOrderer Select();
        IOrderer SelectTop(int count, params string[] parameterNames);
    }
}
