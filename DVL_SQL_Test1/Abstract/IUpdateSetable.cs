namespace DVL_SQL_Test1.Abstract
{
    public interface IUpdateSetable
    {
        IUpdateable Set<TVal>((string, TVal) value);
    }
}
