using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DVL_SQL_Test1.Helpers
{
    public static class DvlSqldatareaderHelpers
    {
        public static List<TResult> AsList<TResult>(SqlDataReader reader, Func<SqlDataReader, TResult> selector)
        {
            var list = new List<TResult>();
            while (reader.Read())
                list.Add(selector(reader));

            return list;
        }

        public static TResult First<TResult>(SqlDataReader reader, Func<SqlDataReader, TResult> selector) =>
            reader.Read()
                ? selector(reader)
                : throw new InvalidOperationException("There was no element in sequence");

        public static TResult FirstOrDefault<TResult>(SqlDataReader reader, Func<SqlDataReader, TResult> selector) =>
            reader.Read() ? selector(reader) : default;

        public static TResult Single<TResult>(SqlDataReader reader, Func<SqlDataReader, TResult> selector) =>
            IsSingleDataReader(reader, selector) switch
            {
                (true, var value) => value,
                _ => throw new InvalidOperationException(
                    "There was no element in sequence or there was more than 1 elements")
            };

        public static TResult SingleOrDefault<TResult>(SqlDataReader reader, Func<SqlDataReader, TResult> selector) =>
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
