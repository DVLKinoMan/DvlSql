using System;
using System.Data;

namespace DVL_SQL_Test1.Models
{

    public sealed class DvlSqlType<TValue>
    {
        public SqlDbType SqlDbType { get; }

        public TValue Value { get; set; }

        public DvlSqlType(TValue value)
        {
            this.Value = value;
            this.SqlDbType = DvlSqlType.DefaultMap(value);
        } 

        public DvlSqlType(TValue value, SqlDbType dbType) : this(value) => this.SqlDbType = dbType;

    }

    public static class DvlSqlType
    {
        //public static readonly DvlSqlType<bool?> Bit = new DvlSqlType<bool?>(SqlDbType.Bit);
        //public static readonly DvlSqlType<DateTime?> DateTime = new DvlSqlType<DateTime?>(SqlDbType.DateTime);
        //public static readonly DvlSqlType<DateTime?> Date = new DvlSqlType<DateTime?>(SqlDbType.Date);
        //public static readonly DvlSqlType<decimal?> Decimal = new DvlSqlType<decimal?>(SqlDbType.Decimal);
        //public static readonly DvlSqlType<int?> Int = new DvlSqlType<int?>(SqlDbType.Int);
        //public static readonly DvlSqlType<Guid?> Guid = new DvlSqlType<Guid?>(SqlDbType.UniqueIdentifier);


        public static SqlDbType DefaultMap<TValue>(TValue value) =>
            value switch
            {
                bool _ => SqlDbType.Bit,
                DateTime _=>SqlDbType.DateTime,
                decimal _=>SqlDbType.Decimal,
                int _=>SqlDbType.Int,
                Guid _=> SqlDbType.UniqueIdentifier,
                _=>throw new NotImplementedException("value is not implemented")
            };

    }
}
