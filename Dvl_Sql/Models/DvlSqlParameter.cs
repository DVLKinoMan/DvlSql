using System;
using System.Data;
using System.Data.SqlClient;


namespace DvlSql.Models
{
    public sealed class DvlSqlParameter<TValue> : DvlSqlParameter
    {
        public bool ExactValue { get; }

        public DvlSqlParameter(string name, DvlSqlType type) : base(CreateParameter(name, type))
        {
            if (type is DvlSqlType<TValue> dvlSqlTypeValue)
                this.ExactValue = dvlSqlTypeValue.ExactValue;
        }

        public DvlSqlParameter(DvlSqlType type) : base(
            CreateParameter(type.Name, type))
        {
            if (type is DvlSqlType<TValue> dvlSqlTypeValue)
                this.ExactValue = dvlSqlTypeValue.ExactValue;
        }

        private static SqlParameter CreateParameter(string name, DvlSqlType type)
        {
            var param = new SqlParameter(name.GetStringAfter("."), type.SqlDbType)
            {
                Direction = ParameterDirection.Input
            };

            if (type is DvlSqlType<TValue> dvlSqlTypeValue)
                param.Value = dvlSqlTypeValue.Value;

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
            var param = new SqlParameter(name.GetStringAfter("."), type.SqlDbType)
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
