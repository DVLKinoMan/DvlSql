using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using DVL_SQL_Test1.Models;

namespace DVL_SQL_Test1.Helpers
{
    public static class DvlSqlHelpers
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

        public static IEnumerable<DvlSqlParameter> Params(params DvlSqlParameter[] parameters) => parameters.Select(param => param);

        //public static DvlSqlParameter Param<TValue>(string parameterName, TValue value) => new DvlSqlParameter<TValue>(parameterName, new DvlSqlType<TValue>(value));

        //public static DvlSqlParameter Param<TValue>(string parameterName, DvlSqlType<TValue> dvlSqlType) => new DvlSqlParameter<TValue>(parameterName, dvlSqlType);

    }
}
