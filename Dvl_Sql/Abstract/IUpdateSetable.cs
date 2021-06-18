using DvlSql.Models;

namespace DvlSql.Abstract
{
    public interface IUpdateSetable
    {
        IUpdateable Set<TVal>(DvlSqlType<TVal> value);
        IUpdateable Set(DvlSqlParameter value);
    }
}
