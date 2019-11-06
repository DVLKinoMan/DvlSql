using DVL_SQL_Test1.Models;

namespace DVL_SQL_Test1.Abstract
{
    public interface IUpdateSetable
    {
        IUpdateable Set<TVal>((string, DvlSqlType<TVal>) value);
    }
}
