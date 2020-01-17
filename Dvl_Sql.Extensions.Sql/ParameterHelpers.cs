using System.Collections.Generic;
using System.Linq;
using Dvl_Sql.Models;

namespace Dvl_Sql.Extensions.Types
{
    public static partial class TypeHelpers
    {
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
    }
}
