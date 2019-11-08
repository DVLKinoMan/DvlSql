using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DVL_SQL_Test1.Helpers
{
    public static class DvlSqlDataReaderHelpers
    {
        public static Func<SqlDataReader, List<TResult>> AsList<TResult>(Func<SqlDataReader, TResult> selector) =>
            reader =>
        {
            var list = new List<TResult>();
            while (reader.Read())
                list.Add(selector(reader));

            return list;
        };

        public static Func<SqlDataReader, TResult> First<TResult>(Func<SqlDataReader, TResult> selector) =>
            reader => reader.Read()
                ? selector(reader)
                : throw new InvalidOperationException("There was no element in sequence");

        public static Func<SqlDataReader, TResult> FirstOrDefault<TResult>(Func<SqlDataReader, TResult> selector) =>
            reader => reader.Read() ? selector(reader) : default;

        public static Func<SqlDataReader, TResult> Single<TResult>(Func<SqlDataReader, TResult> selector) =>
            reader =>
                IsSingleDataReader(reader, selector) switch
                {
                    (true, var value) => value,
                    _ => throw new InvalidOperationException(
                        "There was no element in sequence or there was more than 1 elements")
                };

        public static Func<SqlDataReader, TResult> SingleOrDefault<TResult>(Func<SqlDataReader, TResult> selector) =>
            reader =>
                IsSingleDataReader(reader, selector) switch
                {
                    (true, var value) => value,
                    _ => default
                };

        private static (bool isSingle, TResult result) IsSingleDataReader<TResult>(SqlDataReader reader, Func<SqlDataReader, TResult> func)
        {
            if (!reader.Read())
                return (default, default);

            var firstValue = func(reader);
            return reader.Read() ? (false, firstReader: firstValue) : (true, firstReader: firstValue);
        }
    }
}
