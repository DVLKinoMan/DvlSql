using System;
using System.Data;
using Dvl_Sql.Models;

namespace Dvl_Sql.Extensions.Types
{
    public static partial class TypeHelpers
    {
        public static DvlSqlType<DateTime> DateTime(string name, DateTime value) =>
            new DvlSqlType<DateTime>(name, value, SqlDbType.DateTime);

        public static DvlSqlType DateTimeType(string name) =>
            new DvlSqlType(name, SqlDbType.DateTime);

        public static DvlSqlType<DateTime> DateTime(DateTime value) =>
            new DvlSqlType<DateTime>(value, SqlDbType.DateTime);

        public static DvlSqlType<TimeSpan> DateTime(TimeSpan value) =>
            new DvlSqlType<TimeSpan>(value, SqlDbType.Time);

        public static DvlSqlType<DateTime> DateTime2(string name, DateTime value) =>
            new DvlSqlType<DateTime>(name, value, SqlDbType.DateTime2);

        public static DvlSqlType DateTime2Type(string name) =>
            new DvlSqlType(name, SqlDbType.DateTime2);

        public static DvlSqlType<DateTime> DateTime2(DateTime value) =>
            new DvlSqlType<DateTime>(value, SqlDbType.DateTime2);

        public static DvlSqlType<DateTimeOffset> DateTimeOffset(string name, DateTimeOffset value) =>
            new DvlSqlType<DateTimeOffset>(name, value, SqlDbType.DateTimeOffset);

        public static DvlSqlType DateTimeOffsetType(string name) =>
            new DvlSqlType(name, SqlDbType.DateTimeOffset);

        public static DvlSqlType<DateTimeOffset> DateTimeOffset(DateTimeOffset value) =>
            new DvlSqlType<DateTimeOffset>(value, SqlDbType.DateTimeOffset);

        public static DvlSqlType<DateTime> SmallDateTime(string name, DateTime value) =>
            new DvlSqlType<DateTime>(name, value, SqlDbType.SmallDateTime);

        public static DvlSqlType SmallDateTimeType(string name) =>
            new DvlSqlType(name, SqlDbType.SmallDateTime);

        public static DvlSqlType<DateTime> SmallDateTime(DateTime value) =>
            new DvlSqlType<DateTime>(value, SqlDbType.SmallDateTime);

        public static DvlSqlType<DateTime> Date(string name, DateTime value) =>
            new DvlSqlType<DateTime>(name, value, SqlDbType.Date);

        public static DvlSqlType DateType(string name) =>
            new DvlSqlType(name, SqlDbType.Date);

        public static DvlSqlType<DateTime> Date(DateTime value) =>
            new DvlSqlType<DateTime>(value, SqlDbType.Date);

        public static DvlSqlType<byte[]> Timestamp(byte[] value) =>
            new DvlSqlType<byte[]>(value, SqlDbType.Timestamp);

        public static DvlSqlType TimestampType(string name) =>
            new DvlSqlType(name, SqlDbType.Timestamp);
    }
}
