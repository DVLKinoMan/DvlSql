using System;
using System.Data;
using System.Data.SqlClient;
using DVL_SQL_Test1.Extensions;

namespace DVL_SQL_Test1.Models
{
    public sealed class DvlSqlParameter<TValue> : DvlSqlParameter
    {
        public DvlSqlParameter(string name, DvlSqlType<TValue> type) : base(CreateParameter(name, type))
        {
        }

        public DvlSqlParameter(DvlSqlType<TValue> type) : base(
            CreateParameter(type.Name, type))
        {
        }

        private static SqlParameter CreateParameter(string name, DvlSqlType<TValue> type)
        {
            var param = new SqlParameter(name.WithAlpha(), type.SqlDbType)
            {
                Value = type.Value,
                Direction = ParameterDirection.Input
            };

            if (type.Size != null)
                param.Size = type.Size.Value;

            if (type.Precision != null)
                param.Precision = type.Precision.Value;

            if (type.Scale != null)
                param.Scale = type.Scale.Value;

            return param;
        }
    }

    public sealed class OutputDvlSqlParameter : DvlSqlParameter
    {
        public object Value => this.SqlParameter.Value;

        public OutputDvlSqlParameter(string name, DvlSqlType type) : base(CreateParameter(name, type))
        {
        }

        private static SqlParameter CreateParameter(string name, DvlSqlType type)
        {
            var param = new SqlParameter(name.WithAlpha(), type.SqlDbType)
            {
                Value = DBNull.Value,
                Direction = ParameterDirection.Output
            };

            if (type.Size != null)
                param.Size = type.Size.Value;

            if (type.Precision != null)
                param.Precision = type.Precision.Value;

            if (type.Scale != null)
                param.Scale = type.Scale.Value;

            return param;
        }
    }

    public abstract class DvlSqlParameter
    {
        public SqlParameter SqlParameter { get; }

        public string Name
        {
            get => this.SqlParameter.ParameterName;
            set => this.SqlParameter.ParameterName = value.WithAlpha();
        }

        protected DvlSqlParameter(SqlParameter sqlParameter) => this.SqlParameter = sqlParameter;
    }
}
