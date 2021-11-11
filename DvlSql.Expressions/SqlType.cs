using DvlSql.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DvlSql.Models
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
                _ => value?.ToString()
            };

        //public static DvlSqlType GetDefaultDvlSqlType<TValue>(string name, TValue value) =>
        //    value switch
        //    {
        //        bool b => Bit(name, b),
        //        DateTime d => DateTime(name, d),
        //        decimal dec => Decimal(name, dec),
        //        int i => Int(name, i),
        //        Guid guid => UniqueIdentifier(name, guid),
        //        string str => NVarCharMax(name, str),
        //        byte b => TinyInt(name, b),
        //        byte[] b => Binary(name, b),
        //        _ when typeof(TValue) is {} t && t.GetGenericTypeDefinition() == typeof(Nullable<>)
        //            => GetDefaultDvlSqlType(Nullable.GetUnderlyingType(t), name),
        //        _ => throw new NotImplementedException("value is not implemented")
        //    };

        //public static DvlSqlType GetDefaultDvlSqlType(this Type type, string name) =>
        //    type switch
        //    {
        //        { } t when t == typeof(bool) => BitType(name),
        //        { } t when t == typeof(DateTime) => DateTimeType(name),
        //        { } t when t == typeof(decimal) => DecimalType(name),
        //        { } t when t == typeof(int) => IntType(name),
        //        { } t when t == typeof(byte) => TinyIntType(name),
        //        { } t when t == typeof(byte[]) => BinaryType(name),
        //        { } t when t == typeof(Guid) => UniqueIdentifierType(name),
        //        { } t when t == typeof(string) => NVarCharMaxType(name),
        //        {IsGenericType: true} t when t.GetGenericTypeDefinition() == typeof(Nullable<>)
        //            => GetDefaultDvlSqlType(Nullable.GetUnderlyingType(type), name),
        //        _ => throw new NotImplementedException("value is not implemented")
        //    };

        public static SqlDbType DefaultMap<TValue>(TValue value) =>
            DefaultMap(typeof(TValue) == typeof(object) ? value.GetType() : typeof(TValue));

        //internal static Dictionary<Type, SqlDbType> SqlDbTypes = new Dictionary<Type, SqlDbType>()
        //{
        //    {typeof(bool), SqlDbType.Bit},
        //    {typeof(DateTime), SqlDbType.DateTime},
        //    {typeof(decimal), SqlDbType.Decimal},
        //    {typeof(double), SqlDbType.Float},
        //    {typeof(float), SqlDbType.Float},
        //    {typeof(int), SqlDbType.Int},
        //    {typeof(Guid), SqlDbType.UniqueIdentifier},
        //    {typeof(string), SqlDbType.NVarChar},
        //    {typeof(byte), SqlDbType.TinyInt},
        //    {typeof(byte[]), SqlDbType.Binary}
        //};

        //public static SqlDbType DefaultMap(Type type) => 
        //    SqlDbTypes.ContainsKey(type)
        //    ? SqlDbTypes[type]
        //    : type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)
        //            ? DefaultMap(Nullable.GetUnderlyingType(type))
        //            : throw new NotImplementedException($"{type.Name} is not implemented");

        //public static IEnumerable<DvlSqlParameter> GetSqlParameters(ITuple[] @params, DvlSqlType[] types)
        //{
        //    if (types?.Length == 0)
        //        yield break;

        //    int count = 1;
        //    foreach (var param in @params)
        //    {
        //        foreach (var p in GetSqlParams(param, types, count))
        //            yield return p;
        //        count++;
        //    }
        //}

        //private static IEnumerable<DvlSqlParameter> GetSqlParams(ITuple @param, DvlSqlType[] types, int count)
        //{
        //    var paramType = param.GetType();
        //    for (int i = 0; i < param.Length; i++)
        //    {
        //        var genericTypeArgument = paramType.GenericTypeArguments[i];
        //        if (param[i] is ITuple)
        //        {
        //            foreach (var p in GetSqlParams((ITuple)param[i], types.Skip(i).ToArray(), count))
        //                yield return p;
        //            continue;
        //        }
        //        var type = typeof(DvlSqlType<>).MakeGenericType(genericTypeArgument);
        //        var dvlSqlType =
        //            Activator.CreateInstance(type,
        //                new[] {param[i], types[i], false}); //added false value, maybe not right
        //        var type2 = typeof(DvlSqlParameter<>).MakeGenericType(genericTypeArgument);
        //        string name = $"{types[i].Name.WithAlpha()}{count}";
        //        yield return (DvlSqlParameter) Activator.CreateInstance(type2, new object[] {name, dvlSqlType});
        //    }
        //}

    }
}
