using System.Data;
using Dvl_Sql.Models;

namespace Dvl_Sql.Extensions.Types
{
    public static partial class TypeHelpers
    {
        public static DvlSqlType<bool> Bit(string name, bool value) =>
            new DvlSqlType<bool>(name, value, SqlDbType.Bit);

        public static DvlSqlType<bool> Bit(bool value) =>
            new DvlSqlType<bool>(value, SqlDbType.Bit);

        public static DvlSqlType BitType(string name) =>
            new DvlSqlType(name, SqlDbType.Bit);

        public static DvlSqlType BitType() =>
            new DvlSqlType(SqlDbType.Bit);

        public static DvlSqlType<byte[]> Binary(string name, byte[] value) =>
            new DvlSqlType<byte[]>(name, value, SqlDbType.Binary);

        public static DvlSqlType BinaryType(string name) =>
            new DvlSqlType(name, SqlDbType.Binary);

        public static DvlSqlType BinaryType() =>
            new DvlSqlType(SqlDbType.Binary);

        public static DvlSqlType<byte[]> Binary(byte[] value) =>
            new DvlSqlType<byte[]>(value, SqlDbType.Binary);

        public static DvlSqlType<byte[]> VarBinary(string name, byte[] value) =>
            new DvlSqlType<byte[]>(name, value, SqlDbType.VarBinary);

        public static DvlSqlType VarBinaryType(string name) =>
            new DvlSqlType(name, SqlDbType.VarBinary);

        public static DvlSqlType VarBinaryType() =>
            new DvlSqlType(SqlDbType.VarBinary);

        public static DvlSqlType<byte[]> VarBinary(byte[] value) =>
            new DvlSqlType<byte[]>(value, SqlDbType.VarBinary);

        public static DvlSqlType<byte[]> Image(string name, byte[] value) =>
            new DvlSqlType<byte[]>(name, value, SqlDbType.Image);

        public static DvlSqlType ImageType(string name) =>
            new DvlSqlType(name, SqlDbType.Image);

        public static DvlSqlType ImageType() =>
            new DvlSqlType(SqlDbType.Image);

        public static DvlSqlType<byte[]> Image(byte[] value) =>
            new DvlSqlType<byte[]>(value, SqlDbType.Image);
    }
}
