using System;
using System.Data;
using System.Globalization;

namespace Dvl_Sql.Extensions
{
    internal static class SqlTypeExtensions
    {
        public static string GetDefaultSqlString<TValue>(TValue value) =>
            value switch
            {
                string str => $"'{str}'",
                int i => i.ToString(),
                char ch => $"'{ch}'",
                decimal d => d.ToString(CultureInfo.InvariantCulture),
                DateTime d => $"'{d:yyyy-MM-dd HH:mm:ss}'",
                bool b => $"{(b ? 1 : 0)}",
                _ => value.ToString()
            };

        public static SqlDbType DefaultMap<TValue>(TValue value) =>
            value switch
            {
                bool _ => SqlDbType.Bit,
                DateTime _ => SqlDbType.DateTime,
                decimal _ => SqlDbType.Decimal,
                int _ => SqlDbType.Int,
                Guid _ => SqlDbType.UniqueIdentifier,
                _ => throw new NotImplementedException("value is not implemented")
            };
    }
}
