using System;
using System.Data;
using DVL_SQL_Test1.Extensions;

namespace DVL_SQL_Test1.Models
{
    public class DvlSqlType
    {
        private string _name;

        public string Name
        {
            get => this._name;
            set => this._name = value.WithAlpha();
        }

        public SqlDbType SqlDbType { get; set; }

        public int? Size { get; set; }

        public byte? Precision { get; set; }

        public byte? Scale { get; set; }

        public DvlSqlType(string name, SqlDbType dbType, int? size = null, byte? precision = null, byte? scale = null)
        {
            this.Name = name;
            this.Size = size;
            this.Precision = precision;
            this.Scale = scale;
            this.SqlDbType = dbType;
        }

        public DvlSqlType(SqlDbType dbType, int? size = null, byte? precision = null, byte? scale = null)
        {
            this.Size = size;
            this.Precision = precision;
            this.Scale = scale;
            this.SqlDbType = dbType;
        }
    }

    public sealed class DvlSqlType<TValue> : DvlSqlType
    {
        public TValue Value { get; set; }

        public DvlSqlType(TValue value, DvlSqlType dvlSqlType) : base(dvlSqlType.Name, dvlSqlType.SqlDbType,
            dvlSqlType.Size, dvlSqlType.Precision, dvlSqlType.Scale) => this.Value = value;

        public DvlSqlType(string name, TValue value, int? size = null, byte? precision = null, byte? scale = null) :
            base(name, CustomDvlSqlType.DefaultMap(value), size, precision, scale) =>
            this.Value = value;

        public DvlSqlType(string name, TValue value, SqlDbType dbType, int? size = null, byte? precision = null,
            byte? scale = null) : base(name, dbType, size, precision, scale) =>
            this.Value = value;

        public DvlSqlType(TValue value, SqlDbType dbType, int? size = null, byte? precision = null,
            byte? scale = null) : base(null, dbType, size, precision, scale) =>
            this.Value = value;
    }

    public static class CustomDvlSqlType
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
                DateTime _ => SqlDbType.DateTime,
                decimal _ => SqlDbType.Decimal,
                int _ => SqlDbType.Int,
                Guid _ => SqlDbType.UniqueIdentifier,
                _ => throw new NotImplementedException("value is not implemented")
            };

        public static DvlSqlType<bool> Bit(string name, bool value) => new DvlSqlType<bool>(name, value, SqlDbType.Bit);
        public static DvlSqlType<DateTime> DateTime(string name, DateTime value) => new DvlSqlType<DateTime>(name, value, SqlDbType.DateTime);
        public static DvlSqlType<DateTime> DateTime2(string name, DateTime value) => new DvlSqlType<DateTime>(name, value, SqlDbType.DateTime2);
        public static DvlSqlType<DateTimeOffset> DateTimeOffset(string name, DateTimeOffset value) => new DvlSqlType<DateTimeOffset>(name, value, SqlDbType.DateTimeOffset);
        public static DvlSqlType<DateTime> SmallDateTime(string name, DateTime value) => new DvlSqlType<DateTime>(name, value, SqlDbType.SmallDateTime);
        public static DvlSqlType<DateTime> Date(string name, DateTime value) => new DvlSqlType<DateTime>(name, value, SqlDbType.Date);
        public static DvlSqlType<decimal> Decimal(string name, decimal value, byte? precision = null, byte? scale = null) => new DvlSqlType<decimal>(name, value, SqlDbType.Decimal, precision, scale);
        public static DvlSqlType<decimal> Money(string name, decimal value) => new DvlSqlType<decimal>(name, value, SqlDbType.Money);
        public static DvlSqlType<decimal> SmallMoney(string name, decimal value) => new DvlSqlType<decimal>(name, value, SqlDbType.SmallMoney);
        public static DvlSqlType<double> Float(string name, double value) => new DvlSqlType<double>(name, value, SqlDbType.Float);
        public static DvlSqlType<int> Int(string name, int value) => new DvlSqlType<int>(name, value, SqlDbType.Int);
        public static DvlSqlType<Guid> UniqueIdentifier(string name, Guid value) => new DvlSqlType<Guid>(name, value, SqlDbType.UniqueIdentifier);
        public static DvlSqlType<long> BigInt(string name, long value) => new DvlSqlType<long>(name, value, SqlDbType.BigInt);
        public static DvlSqlType<byte[]> Binary(string name, byte[] value) => new DvlSqlType<byte[]>(name, value, SqlDbType.Binary);
        public static DvlSqlType<byte[]> VarBinary(string name, byte[] value) => new DvlSqlType<byte[]>(name, value, SqlDbType.VarBinary);
        public static DvlSqlType<byte[]> Image(string name, byte[] value) => new DvlSqlType<byte[]>(name, value, SqlDbType.Image);
        public static DvlSqlType<string> NVarChar(string name, string value, int size) => new DvlSqlType<string>(name, value, SqlDbType.NVarChar, size);
        public static DvlSqlType<string> NVarCharMax(string name, string value) => NVarChar(name, value, -1);
        public static DvlSqlType<string> VarChar(string name, string value, int size) => new DvlSqlType<string>(name, value, SqlDbType.VarChar, size);
        public static DvlSqlType<string> VarCharMax(string name, string value) => NVarChar(name, value, -1);
        public static DvlSqlType<string> Char(string name, string value, int size) => new DvlSqlType<string>(name, value, SqlDbType.Char, size);
        public static DvlSqlType<string> CharMax(string name, string value) => Char(name, value, -1);
        public static DvlSqlType<string> NChar(string name, string value, int size) => new DvlSqlType<string>(name, value, SqlDbType.NChar, size);
        public static DvlSqlType<string> NCharMax(string name, string value) => NChar(name, value, -1);

        public static DvlSqlType<string> Text(string name, string value) => value.Length <= Math.Pow(2, 31) - 1
            ? new DvlSqlType<string>(name, value, SqlDbType.Text)
            : throw new ArgumentException("value length not allowed");

        public static DvlSqlType<string> NText(string name, string value) => value.Length <= Math.Pow(2, 30) - 1
            ? new DvlSqlType<string>(name, value, SqlDbType.NText)
            : throw new ArgumentException("value length not allowed");



        public static DvlSqlType<bool> Bit(bool value) => new DvlSqlType<bool>(value, SqlDbType.Bit);
        public static DvlSqlType<DateTime> DateTime(DateTime value) => new DvlSqlType<DateTime>(value, SqlDbType.DateTime);
        public static DvlSqlType<DateTime> DateTime2(DateTime value) => new DvlSqlType<DateTime>(value, SqlDbType.DateTime2);
        public static DvlSqlType<DateTimeOffset> DateTimeOffset(DateTimeOffset value) => new DvlSqlType<DateTimeOffset>(value, SqlDbType.DateTimeOffset);
        public static DvlSqlType<DateTime> SmallDateTime(DateTime value) => new DvlSqlType<DateTime>(value, SqlDbType.SmallDateTime);
        public static DvlSqlType<DateTime> Date(DateTime value) => new DvlSqlType<DateTime>(value, SqlDbType.Date);
        public static DvlSqlType<decimal> Decimal(decimal value, byte? precision = null, byte? scale = null) => new DvlSqlType<decimal>(value, SqlDbType.Decimal, precision, scale);
        public static DvlSqlType<decimal> Money(decimal value) => new DvlSqlType<decimal>(value, SqlDbType.Money);
        public static DvlSqlType<decimal> SmallMoney(decimal value) => new DvlSqlType<decimal>(value, SqlDbType.SmallMoney);
        public static DvlSqlType<double> Float(double value) => new DvlSqlType<double>(value, SqlDbType.Float);
        public static DvlSqlType<int> Int(int value) => new DvlSqlType<int>(value, SqlDbType.Int);
        public static DvlSqlType<Guid> UniqueIdentifier(Guid value) => new DvlSqlType<Guid>(value, SqlDbType.UniqueIdentifier);
        public static DvlSqlType<long> BigInt(long value) => new DvlSqlType<long>(value, SqlDbType.BigInt);
        public static DvlSqlType<byte[]> Binary(byte[] value) => new DvlSqlType<byte[]>(value, SqlDbType.Binary);
        public static DvlSqlType<byte[]> VarBinary(byte[] value) => new DvlSqlType<byte[]>(value, SqlDbType.VarBinary);
        public static DvlSqlType<byte[]> Image(byte[] value) => new DvlSqlType<byte[]>(value, SqlDbType.Image);
        public static DvlSqlType<string> NVarChar(string value, int size) => new DvlSqlType<string>(value, SqlDbType.NVarChar, size);
        public static DvlSqlType<string> NVarCharMax(string value) => NVarChar(value, -1);
        public static DvlSqlType<string> VarChar(string value, int size) => new DvlSqlType<string>(value, SqlDbType.VarChar, size);
        public static DvlSqlType<string> VarCharMax(string value) => NVarChar(value, -1);
        public static DvlSqlType<string> Char(string value, int size) => new DvlSqlType<string>(value, SqlDbType.Char, size);
        public static DvlSqlType<string> CharMax(string value) => Char(value, -1);
        public static DvlSqlType<string> NChar(string value, int size) => new DvlSqlType<string>(value, SqlDbType.NChar, size);
        public static DvlSqlType<string> NCharMax(string value) => NChar(value, -1);

        public static DvlSqlType<string> Text(string value) => value.Length <= Math.Pow(2, 31) - 1
            ? new DvlSqlType<string>(value, SqlDbType.Text)
            : throw new ArgumentException("value length not allowed");

        public static DvlSqlType<string> NText(string value) => value.Length <= Math.Pow(2, 30) - 1
            ? new DvlSqlType<string>(value, SqlDbType.NText)
            : throw new ArgumentException("value length not allowed");

        public static DvlSqlType<TimeSpan> DateTime(TimeSpan value) => new DvlSqlType<TimeSpan>(value, SqlDbType.Time);

        public static DvlSqlType<byte[]> Timestamp(byte[] value) => new DvlSqlType<byte[]>(value, SqlDbType.Timestamp);

        public static DvlSqlType<byte> TinyInt(byte value) => new DvlSqlType<byte>(value, SqlDbType.TinyInt);

        public static DvlSqlType<string> Xml(string value) => new DvlSqlType<string>(value, SqlDbType.Xml);
    }
}
