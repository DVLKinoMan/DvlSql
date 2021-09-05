using DvlSql.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DvlSql
{
    public static class SqlType
    {
        public static string GetDefaultSqlString<TValue>(TValue value) =>
            value switch
            {
                //string str => $"'{str}'",
                //int i => i.ToString(),
                //char ch => $"'{ch}'",
                decimal d => d.ToString(CultureInfo.InvariantCulture),
                Guid guid => $"'{guid}'",
                //DateTime d => $"'{d:yyyy-MM-dd HH:mm:ss}'",
                bool b => $"{(b ? 1 : 0)}",
                _ => value.ToString()
            };

        public static DvlSqlType GetDefaultDvlSqlType<TValue>(string name, TValue value) =>
            value switch
            {
                bool b => Bit(name, b),
                DateTime d => DateTime(name, d),
                decimal dec => Decimal(name, dec),
                int i => Int(name, i),
                Guid guid => UniqueIdentifier(name, guid),
                string str => NVarCharMax(name, str),
                byte b => TinyInt(name, b),
                byte[] b => Binary(name, b),
                _ when typeof(TValue) is {} t && t.GetGenericTypeDefinition() == typeof(Nullable<>)
                    => GetDefaultDvlSqlType(Nullable.GetUnderlyingType(t), name),
                _ => throw new NotImplementedException("value is not implemented")
            };

        public static DvlSqlType GetDefaultDvlSqlType(this Type type, string name) =>
            type switch
            {
                { } t when t == typeof(bool) => BitType(name),
                { } t when t == typeof(DateTime) => DateTimeType(name),
                { } t when t == typeof(decimal) => DecimalType(name),
                { } t when t == typeof(int) => IntType(name),
                { } t when t == typeof(byte) => TinyIntType(name),
                { } t when t == typeof(byte[]) => BinaryType(name),
                { } t when t == typeof(Guid) => UniqueIdentifierType(name),
                { } t when t == typeof(string) => NVarCharMaxType(name),
                {IsGenericType: true} t when t.GetGenericTypeDefinition() == typeof(Nullable<>)
                    => GetDefaultDvlSqlType(Nullable.GetUnderlyingType(type), name),
                _ => throw new NotImplementedException("value is not implemented")
            };

        internal static SqlDbType DefaultMap<TValue>(TValue value) =>
            DefaultMap(typeof(TValue) == typeof(object) ? value.GetType() : typeof(TValue));

        internal static Dictionary<Type, SqlDbType> SqlDbTypes = new Dictionary<Type, SqlDbType>()
        {
            {typeof(bool), SqlDbType.Bit},
            {typeof(DateTime), SqlDbType.DateTime},
            {typeof(decimal), SqlDbType.Decimal},
            {typeof(int), SqlDbType.Int},
            {typeof(Guid), SqlDbType.UniqueIdentifier},
            {typeof(string), SqlDbType.NVarChar},
            {typeof(byte), SqlDbType.TinyInt},
            {typeof(byte[]), SqlDbType.Binary}
        };

        internal static SqlDbType DefaultMap(Type type) => 
            SqlDbTypes.ContainsKey(type)
            ? SqlDbTypes[type]
            : type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                    ? DefaultMap(Nullable.GetUnderlyingType(type))
                    : throw new NotImplementedException($"{type.Name} is not implemented");

        public static IEnumerable<DvlSqlParameter> GetSqlParameters(ITuple[] @params, DvlSqlType[] types)
        {
            if(types?.Length == 0)
                yield break;

            int count = 1;
            foreach (var param in @params)
            {
                var paramType = param.GetType();
                for (int i = 0; i < param.Length; i++)
                {
                    var genericTypeArgument = paramType.GenericTypeArguments[i];
                    var type = typeof(DvlSqlType<>).MakeGenericType(genericTypeArgument);
                    var dvlSqlType =
                        Activator.CreateInstance(type,
                            new[] { param[i], types[i], false }); //added false value, maybe not right
                    var type2 = typeof(DvlSqlParameter<>).MakeGenericType(genericTypeArgument);
                    string name = $"{types[i].Name.WithAlpha()}{count}";
                    yield return (DvlSqlParameter)Activator.CreateInstance(type2, new object[] { name, dvlSqlType });
                }

                count++;
            }
        }

        #region Binary
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
        #endregion
        
        #region Money
        public static DvlSqlType<decimal> Money(string name, decimal value) =>
            new DvlSqlType<decimal>(name, value, SqlDbType.Money);

        public static DvlSqlType MoneyType(string name) =>
            new DvlSqlType(name, SqlDbType.Money);

        public static DvlSqlType MoneyType() =>
            new DvlSqlType(SqlDbType.Money);

        public static DvlSqlType<decimal> Money(decimal value) =>
            new DvlSqlType<decimal>(value, SqlDbType.Money);

        public static DvlSqlType<decimal> SmallMoney(string name, decimal value) =>
            new DvlSqlType<decimal>(name, value, SqlDbType.SmallMoney);

        public static DvlSqlType SmallMoneyType(string name) =>
            new DvlSqlType(name, SqlDbType.SmallMoney);

        public static DvlSqlType SmallMoneyType() =>
            new DvlSqlType(SqlDbType.SmallMoney);

        public static DvlSqlType<decimal> SmallMoney(decimal value) =>
            new DvlSqlType<decimal>(value, SqlDbType.SmallMoney);
        #endregion
        
        #region Number
        public static DvlSqlType<decimal> Decimal(string name, decimal value, byte? precision = null,
            byte? scale = null) => new DvlSqlType<decimal>(name, value, SqlDbType.Decimal, precision: precision, scale: scale);

        public static DvlSqlType DecimalType(string name, bool? isNotNull = null, byte? precision = null,
            byte? scale = null) => new DvlSqlType(name, SqlDbType.Decimal, isNotNull: isNotNull, precision: precision, scale: scale);

        public static DvlSqlType DecimalType() => 
            new DvlSqlType(SqlDbType.Decimal);

        public static DvlSqlType<decimal> Decimal(decimal value,byte? precision = null, byte? scale = null) =>
            new DvlSqlType<decimal>(value, SqlDbType.Decimal, precision: precision, scale:scale);

        public static DvlSqlType<double> Float(string name, double value) =>
            new DvlSqlType<double>(name, value, SqlDbType.Float);

        public static DvlSqlType FloatType(string name) =>
            new DvlSqlType(name, SqlDbType.Float);

        public static DvlSqlType FloatType() =>
            new DvlSqlType(SqlDbType.Float);

        public static DvlSqlType<double> Float(double value) =>
            new DvlSqlType<double>(value, SqlDbType.Float);

        public static DvlSqlType<int> Int(string name, int value) =>
            new DvlSqlType<int>(name, value, SqlDbType.Int);

        public static DvlSqlType<int> Int(int value) =>
            new DvlSqlType<int>(value, SqlDbType.Int);

        public static DvlSqlType IntType(string name, bool? isNotNull = null) =>
            new DvlSqlType(name, SqlDbType.Int, isNotNull: isNotNull);

        public static DvlSqlType IntType() =>
            new DvlSqlType(SqlDbType.Int);

        public static DvlSqlType<byte> TinyInt(string name, byte value) =>
            new DvlSqlType<byte>(name, value, SqlDbType.TinyInt);
        
        public static DvlSqlType<byte> TinyInt(byte value) =>
            new DvlSqlType<byte>(value, SqlDbType.TinyInt);

        public static DvlSqlType TinyIntType(string name) =>
            new DvlSqlType(name, SqlDbType.TinyInt);

        public static DvlSqlType TinyIntType() =>
            new DvlSqlType(SqlDbType.TinyInt);

        public static DvlSqlType<long> BigInt(string name, long value) =>
            new DvlSqlType<long>(name, value, SqlDbType.BigInt);

        public static DvlSqlType BigIntType(string name) =>
            new DvlSqlType(name, SqlDbType.BigInt);

        public static DvlSqlType BigIntType() =>
            new DvlSqlType(SqlDbType.BigInt);

        public static DvlSqlType<long> BigInt(long value) =>
            new DvlSqlType<long>(value, SqlDbType.BigInt);
        #endregion
        
        #region Parameters
        public static IEnumerable<DvlSqlParameter> Params(params DvlSqlParameter[] parameters) =>
            parameters.Select(param => param);

        public static DvlSqlParameter Param<TValue>(string parameterName, TValue value) =>
            new DvlSqlParameter<TValue>(
                parameterName,
                new DvlSqlType<TValue>(parameterName, value));

        public static DvlSqlParameter Param<TValue>(string parameterName, DvlSqlType<TValue> dvlSqlType) =>
            new DvlSqlParameter<TValue>(parameterName, dvlSqlType);

        public static OutputDvlSqlParameter OutputParam(string parameterName, DvlSqlType dvlSqlType) =>
            new OutputDvlSqlParameter(parameterName, dvlSqlType);
        #endregion
        
        #region Text
        public static DvlSqlType<string> NVarChar(string name, string value, int size) =>
            new DvlSqlType<string>(name, value, SqlDbType.NVarChar, size);

        /// <summary>
        /// NVarChar with the exact value which is value parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static DvlSqlType<string> NVarCharWithExactValue(string name, string value, int size) =>
            new DvlSqlType<string>(name, value, SqlDbType.NVarChar, size, exactValue: true);

        public static DvlSqlType<string> NVarChar(string value, int size, bool withExactValue = false) =>
            new DvlSqlType<string>(value, SqlDbType.NVarChar, size, exactValue: withExactValue);

        public static DvlSqlType NVarCharType(string name, int size) =>
            new DvlSqlType(name, SqlDbType.NVarChar, size);

        public static DvlSqlType NVarCharType(int size) =>
            new DvlSqlType(SqlDbType.NVarChar, size);

        public static DvlSqlType<string> NVarCharMax(string name, string value) =>
            NVarChar(name, value, -1);

        public static DvlSqlType<string> NVarCharMax(string value) =>
            new DvlSqlType<string>(value, SqlDbType.NVarChar, -1);

        public static DvlSqlType NVarCharMaxType(string name) =>
            new DvlSqlType(name, SqlDbType.NVarChar, -1);

        public static DvlSqlType NVarCharMaxType() =>
            new DvlSqlType(SqlDbType.NVarChar, -1);

        public static DvlSqlType<string> VarChar(string name, string value, int size) =>
            new DvlSqlType<string>(name, value, SqlDbType.VarChar, size);

        public static DvlSqlType VarCharType(string name, int size) =>
            new DvlSqlType(name, SqlDbType.VarChar, size);

        public static DvlSqlType VarCharType(int size) =>
            new DvlSqlType(SqlDbType.VarChar, size);

        public static DvlSqlType<string> VarChar(string value, int size) =>
            new DvlSqlType<string>(value, SqlDbType.VarChar, size);

        public static DvlSqlType<string> VarCharMax(string value) =>
            VarChar(value, -1);

        public static DvlSqlType VarCharMaxType(string name) =>
            VarCharType(name, -1);

        public static DvlSqlType VarCharMaxType() =>
            VarCharType(-1);

        public static DvlSqlType<string> VarCharMax(string name, string value) =>
            VarChar(name, value, -1);

        public static DvlSqlType<string> Char(string name, string value, int size) =>
            new DvlSqlType<string>(name, value, SqlDbType.Char, size);

        public static DvlSqlType CharType(string name, int size) =>
            new DvlSqlType(name, SqlDbType.Char, size);

        public static DvlSqlType CharType(int size) =>
            new DvlSqlType(SqlDbType.Char, size);

        public static DvlSqlType<string> Char(string value, int size) =>
            new DvlSqlType<string>(value, SqlDbType.Char, size);

        public static DvlSqlType<string> CharMax(string name, string value) =>
            Char(name, value, -1);

        public static DvlSqlType<string> CharMax(string value) =>
            Char(value, -1);

        public static DvlSqlType CharMaxType(string name) =>
            CharType(name, -1);

        public static DvlSqlType CharMaxType() =>
            CharType(-1);

        public static DvlSqlType<string> NChar(string name, string value, int size) =>
            new DvlSqlType<string>(name, value, SqlDbType.NChar, size);

        public static DvlSqlType<string> NChar(string value, int size) =>
            new DvlSqlType<string>(value, SqlDbType.NChar, size);

        public static DvlSqlType NCharType(string name, int size) =>
            new DvlSqlType(name, SqlDbType.NChar, size);

        public static DvlSqlType NCharType(int size) =>
            new DvlSqlType(SqlDbType.NChar, size);

        public static DvlSqlType<string> NCharMax(string name, string value) =>
            NChar(name, value, -1);

        public static DvlSqlType<string> NCharMax(string value) =>
            NChar(value, -1);

        public static DvlSqlType NCharMaxType(string name) =>
            NCharType(name, -1);

        public static DvlSqlType NCharMaxType() =>
            NCharType(-1);

        public static DvlSqlType<string> Text(string name, string value) => value.Length <= Math.Pow(2, 31) - 1
            ? new DvlSqlType<string>(name, value, SqlDbType.Text)
            : throw new ArgumentException("value length not allowed");

        public static DvlSqlType TextType(string name) =>
            new DvlSqlType(name, SqlDbType.Text);

        public static DvlSqlType TextType() =>
            new DvlSqlType(SqlDbType.Text);

        public static DvlSqlType<string> Text(string value) => value.Length <= Math.Pow(2, 31) - 1
            ? new DvlSqlType<string>(value, SqlDbType.Text)
            : throw new ArgumentException("value length not allowed");

        public static DvlSqlType<string> NText(string name, string value) => value.Length <= Math.Pow(2, 30) - 1
            ? new DvlSqlType<string>(name, value, SqlDbType.NText)
            : throw new ArgumentException("value length not allowed");

        public static DvlSqlType NTextType(string name) =>
            new DvlSqlType(name, SqlDbType.NText);

        public static DvlSqlType NTextType() =>
            new DvlSqlType(SqlDbType.NText);

        public static DvlSqlType<string> NText(string value) => value.Length <= Math.Pow(2, 30) - 1
            ? new DvlSqlType<string>(value, SqlDbType.NText)
            : throw new ArgumentException("value length not allowed");
        #endregion
        
        #region Time
        public static DvlSqlType<DateTime> DateTime(string name, DateTime value) =>
            new DvlSqlType<DateTime>(name, value, SqlDbType.DateTime);

        public static DvlSqlType DateTimeType(string name) =>
            new DvlSqlType(name, SqlDbType.DateTime);

        public static DvlSqlType DateTimeType() =>
            new DvlSqlType(SqlDbType.DateTime);

        public static DvlSqlType<DateTime> DateTime(DateTime value) =>
            new DvlSqlType<DateTime>(value, SqlDbType.DateTime);

        public static DvlSqlType<TimeSpan> DateTime(TimeSpan value) =>
            new DvlSqlType<TimeSpan>(value, SqlDbType.Time);

        public static DvlSqlType<DateTime> DateTime2(string name, DateTime value) =>
            new DvlSqlType<DateTime>(name, value, SqlDbType.DateTime2);

        public static DvlSqlType DateTime2Type(string name) =>
            new DvlSqlType(name, SqlDbType.DateTime2);

        public static DvlSqlType DateTime2Type() =>
            new DvlSqlType(SqlDbType.DateTime2);

        public static DvlSqlType<DateTime> DateTime2(DateTime value) =>
            new DvlSqlType<DateTime>(value, SqlDbType.DateTime2);

        public static DvlSqlType<DateTimeOffset> DateTimeOffset(string name, DateTimeOffset value) =>
            new DvlSqlType<DateTimeOffset>(name, value, SqlDbType.DateTimeOffset);

        public static DvlSqlType DateTimeOffsetType(string name) =>
            new DvlSqlType(name, SqlDbType.DateTimeOffset);

        public static DvlSqlType DateTimeOffsetType() =>
            new DvlSqlType(SqlDbType.DateTimeOffset);

        public static DvlSqlType<DateTimeOffset> DateTimeOffset(DateTimeOffset value) =>
            new DvlSqlType<DateTimeOffset>(value, SqlDbType.DateTimeOffset);

        public static DvlSqlType<DateTime> SmallDateTime(string name, DateTime value) =>
            new DvlSqlType<DateTime>(name, value, SqlDbType.SmallDateTime);

        public static DvlSqlType SmallDateTimeType() =>
            new DvlSqlType(SqlDbType.SmallDateTime);

        public static DvlSqlType<DateTime> SmallDateTime(DateTime value) =>
            new DvlSqlType<DateTime>(value, SqlDbType.SmallDateTime);

        public static DvlSqlType<DateTime> Date(string name, DateTime value) =>
            new DvlSqlType<DateTime>(name, value, SqlDbType.Date);

        public static DvlSqlType DateType(string name) =>
            new DvlSqlType(name, SqlDbType.Date);

        public static DvlSqlType DateType() =>
            new DvlSqlType(SqlDbType.Date);

        public static DvlSqlType<DateTime> Date(DateTime value) =>
            new DvlSqlType<DateTime>(value, SqlDbType.Date);

        public static DvlSqlType<byte[]> Timestamp(byte[] value) =>
            new DvlSqlType<byte[]>(value, SqlDbType.Timestamp);

        public static DvlSqlType TimestampType(string name) =>
            new DvlSqlType(name, SqlDbType.Timestamp);

        public static DvlSqlType TimestampType() =>
            new DvlSqlType(SqlDbType.Timestamp);
        #endregion
        
        #region UniqueIdentifier
        public static DvlSqlType<Guid> UniqueIdentifier(string name, Guid value) =>
            new DvlSqlType<Guid>(name, value, SqlDbType.UniqueIdentifier);

        public static DvlSqlType UniqueIdentifierType(string name) =>
            new DvlSqlType(name, SqlDbType.UniqueIdentifier);

        public static DvlSqlType UniqueIdentifierType() =>
            new DvlSqlType(SqlDbType.UniqueIdentifier);

        public static DvlSqlType<Guid> UniqueIdentifier(Guid value) =>
            new DvlSqlType<Guid>(value, SqlDbType.UniqueIdentifier);
        #endregion
        
        #region Xml
        public static DvlSqlType<string> Xml(string name, string value) =>
            new DvlSqlType<string>(name, value, SqlDbType.Xml);

        public static DvlSqlType XmlType(string name) =>
            new DvlSqlType(name, SqlDbType.Xml);

        public static DvlSqlType XmlType() =>
            new DvlSqlType(SqlDbType.Xml);

        public static DvlSqlType<string> Xml(string value) =>
            new DvlSqlType<string>(value, SqlDbType.Xml);
        #endregion
    }
}
