using System.Data.SqlClient;

namespace DVL_SQL_Test1.Models
{
    public sealed class DvlSqlParameter<TValue> : DvlSqlParameter
    {
        public DvlSqlParameter(string name, DvlSqlType<TValue> type) : base(CreateParameter(name, type))
        {
        }

        private static SqlParameter CreateParameter(string name, DvlSqlType<TValue> type)
        {
            var param = new SqlParameter(name, type.SqlDbType)
            {
                Value = type.Value
            };
            return param;
        }
    }

    public abstract class DvlSqlParameter
    {
        public SqlParameter SqlParameter { get; }

        protected DvlSqlParameter(SqlParameter sqlParameter) => this.SqlParameter = sqlParameter;
    }
}
