using System.Collections.Generic;

using System.Data;
using static System.Exts.Extensions;

namespace DvlSql
{
    public class DvlSqlType
    {
        public string Name { get; }

        public SqlDbType SqlDbType { get; }

        public int? Size { get; }

        public bool IsNotNull { get; } = false;

        public byte? Precision { get; }

        public byte? Scale { get;  }

        public DvlSqlType(string name, SqlDbType dbType, int? size = null, bool? isNotNull = null, byte? precision = null, byte? scale = null)
        {
            this.Name = name;
            this.Size = size;
            this.Precision = precision;
            this.Scale = scale;
            this.SqlDbType = dbType;
            this.IsNotNull = isNotNull ?? IsNotNull;
        }

        public DvlSqlType(SqlDbType dbType, int? size = null, bool? isNotNull = null, byte? precision = null, byte? scale = null)
        {
            this.Size = size;
            this.Precision = precision;
            this.Scale = scale;
            this.SqlDbType = dbType;
            this.IsNotNull = isNotNull ?? IsNotNull;
        }

        
        public override bool Equals(object obj) => 
            obj is DvlSqlType type && Equals(type);

        private bool Equals(DvlSqlType other) =>
            other.Name == this.Name &&
            other.Precision == this.Precision &&
            other.Scale == this.Scale &&
            other.Size == this.Size &&
            other.SqlDbType == this.SqlDbType;

        public override int GetHashCode() => this.Name.GetHashCode() +
                                             this.Precision.GetHashCode() +
                                             this.Scale.GetHashCode() +
                                             this.Size.GetHashCode() +
                                             this.SqlDbType.GetHashCode();
    }

    public sealed class DvlSqlType<TValue> : DvlSqlType
    {
        public TValue Value { get; }
        public bool ExactValue { get; }

        public DvlSqlType(TValue value, DvlSqlType dvlSqlType, bool exactValue = false) : base(dvlSqlType.Name, dvlSqlType.SqlDbType,
            dvlSqlType.Size, dvlSqlType.IsNotNull, dvlSqlType.Precision, dvlSqlType.Scale)
        {
            this.Value = value;
            this.ExactValue = exactValue;
        }

        public DvlSqlType(string name, TValue value, int? size = null, bool? isNotNull = null, byte? precision = null, byte? scale = null, bool exactValue = false) :
            base(name, DefaultMap(value), size, isNotNull, precision, scale)
        {
            this.Value = value;
            this.ExactValue = exactValue;
        }

        public DvlSqlType(string name, TValue value, SqlDbType dbType, int? size = null, bool? isNotNull = null, byte? precision = null,
            byte? scale = null, bool exactValue = false) : base(name, dbType, size, isNotNull, precision, scale)
        {
            this.Value = value;
            this.ExactValue = exactValue;
        }

        public DvlSqlType(TValue value, SqlDbType dbType, int? size = null, bool? isNotNull = null, byte? precision = null,
            byte? scale = null, bool exactValue = false) : base(null, dbType, size, isNotNull, precision, scale)
        {
            this.Value = value;
            this.ExactValue = exactValue;
        }

        public override bool Equals(object obj) => 
            obj is DvlSqlType<TValue> type && Equals(type);

        private bool Equals(DvlSqlType<TValue> other) =>
            other.Value.Equals(this.Value) &&
            base.Equals(other);

        public override int GetHashCode() => EqualityComparer<TValue>.Default.GetHashCode(Value) + base.GetHashCode();
    }

}
