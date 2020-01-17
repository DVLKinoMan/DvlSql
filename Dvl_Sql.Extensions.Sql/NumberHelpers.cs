using System.Data;
using Dvl_Sql.Models;

namespace Dvl_Sql.Extensions.Types
{
    public static partial class TypeHelpers
    {
        public static DvlSqlType<decimal> Decimal(string name, decimal value, byte? precision = null,
            byte? scale = null) => new DvlSqlType<decimal>(name, value, SqlDbType.Decimal, precision, scale);

        public static DvlSqlType DecimalType(string name, byte? precision = null,
            byte? scale = null) => new DvlSqlType(name, SqlDbType.Decimal, precision, scale);

        public static DvlSqlType<decimal> Decimal(decimal value, byte? precision = null, byte? scale = null) =>
            new DvlSqlType<decimal>(value, SqlDbType.Decimal, precision, scale);

        public static DvlSqlType<double> Float(string name, double value) =>
            new DvlSqlType<double>(name, value, SqlDbType.Float);

        public static DvlSqlType FloatType(string name) =>
            new DvlSqlType(name, SqlDbType.Float);

        public static DvlSqlType<double> Float(double value) =>
            new DvlSqlType<double>(value, SqlDbType.Float);

        public static DvlSqlType<int> Int(string name, int value) =>
            new DvlSqlType<int>(name, value, SqlDbType.Int);

        public static DvlSqlType<int> Int(int value) =>
            new DvlSqlType<int>(value, SqlDbType.Int);

        public static DvlSqlType IntType(string name) =>
            new DvlSqlType(name, SqlDbType.Int);

        public static DvlSqlType<byte> TinyInt(byte value) =>
            new DvlSqlType<byte>(value, SqlDbType.TinyInt);

        public static DvlSqlType TinyIntType(string name) =>
            new DvlSqlType(name, SqlDbType.TinyInt);

        public static DvlSqlType<long> BigInt(string name, long value) =>
            new DvlSqlType<long>(name, value, SqlDbType.BigInt);

        public static DvlSqlType BigIntType(string name) =>
            new DvlSqlType(name, SqlDbType.BigInt);

        public static DvlSqlType<long> BigInt(long value) =>
            new DvlSqlType<long>(value, SqlDbType.BigInt);
    }
}
