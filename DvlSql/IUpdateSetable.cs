namespace DvlSql;

public interface IUpdateSetable
{
    IUpdateable Set<TVal>(DvlSqlType<TVal> value);
    IUpdateable Set(DvlSqlParameter value);
}
