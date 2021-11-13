using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static System.Exts.Extensions;

namespace DvlSql.SqlServer
{
    internal static class Exts
    {
        internal static IEnumerable<SqlParameter> ToSqlParameters(this IEnumerable<DvlSqlParameter> parameters) =>
            parameters.Select(param => param.ToSqlParameter());

        internal static SqlParameter ToSqlParameter(this DvlSqlParameter parameter)
        {
            var isOuput = parameter is DvlSqlOutputParameter;
            var param = new SqlParameter(parameter.Name.GetStringAfter("."), parameter.DvlSqlType.SqlDbType)
            {
                Direction = isOuput ? ParameterDirection.Output : ParameterDirection.Input
            };

            if (isOuput)
                param.Value = DBNull.Value;
            else if (parameter.DvlSqlType.GetType().GetGenericTypeDefinition() == typeof(DvlSqlType<>))
            {
                var prop = parameter.DvlSqlType.GetType().GetProperty("Value");
                param.Value = prop.GetValue(parameter.DvlSqlType) ?? DBNull.Value;
            }

            if (parameter.DvlSqlType.Size != null)
                param.Size = parameter.DvlSqlType.Size.Value;

            if (parameter.DvlSqlType.Precision != null)
                param.Precision = parameter.DvlSqlType.Precision.Value;

            if (parameter.DvlSqlType.Scale != null)
                param.Scale = parameter.DvlSqlType.Scale.Value;

            return param;
        }
    }
}
