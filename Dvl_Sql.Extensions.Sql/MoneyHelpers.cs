using System.Data;
using Dvl_Sql.Models;

namespace Dvl_Sql.Extensions.Types
{
    public static partial class TypeHelpers
    {
        public static DvlSqlType<decimal> Money(string name, decimal value) =>
            new DvlSqlType<decimal>(name, value, SqlDbType.Money);

        public static DvlSqlType MoneyType(string name) =>
            new DvlSqlType(name, SqlDbType.Money);

        public static DvlSqlType<decimal> Money(decimal value) =>
            new DvlSqlType<decimal>(value, SqlDbType.Money);

        public static DvlSqlType<decimal> SmallMoney(string name, decimal value) =>
            new DvlSqlType<decimal>(name, value, SqlDbType.SmallMoney);

        public static DvlSqlType SmallMoneyType(string name) =>
            new DvlSqlType(name, SqlDbType.SmallMoney);

        public static DvlSqlType<decimal> SmallMoney(decimal value) =>
            new DvlSqlType<decimal>(value, SqlDbType.SmallMoney);
    }
}
