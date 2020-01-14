using Dvl_Sql.Helpers;
using System.Data;

namespace Dvl_Sql.Models
{
    public class DvlSqlType
    {
        public string Name { get; set; }

        public SqlDbType SqlDbType { get; set; }

        public int? Size { get; set; }

        public byte? Precision { get; set; }

        public byte? Scale { get; set; }

        public DvlSqlType(string name, SqlDbType dbType, int? size = null, byte? precision = null, byte? scale = null)
        {
            this.Name = name;
            this.Size = size;
            this.Precision = precision;
            this.Scale = scale;
            this.SqlDbType = dbType;
        }

        public DvlSqlType(SqlDbType dbType, int? size = null, byte? precision = null, byte? scale = null)
        {
            this.Size = size;
            this.Precision = precision;
            this.Scale = scale;
            this.SqlDbType = dbType;
        }
    }

    public sealed class DvlSqlType<TValue> : DvlSqlType
    {
        public TValue Value { get; set; }

        public DvlSqlType(TValue value, DvlSqlType dvlSqlType) : base(dvlSqlType.Name, dvlSqlType.SqlDbType,
            dvlSqlType.Size, dvlSqlType.Precision, dvlSqlType.Scale) => this.Value = value;

        public DvlSqlType(string name, TValue value, int? size = null, byte? precision = null, byte? scale = null) :
            base(name, DvlSqlHelpers.DefaultMap(value), size, precision, scale) =>
            this.Value = value;

        public DvlSqlType(string name, TValue value, SqlDbType dbType, int? size = null, byte? precision = null,
            byte? scale = null) : base(name, dbType, size, precision, scale) =>
            this.Value = value;

        public DvlSqlType(TValue value, SqlDbType dbType, int? size = null, byte? precision = null,
            byte? scale = null) : base(null, dbType, size, precision, scale) =>
            this.Value = value;
    }

}
