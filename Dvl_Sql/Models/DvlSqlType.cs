using System;
using System.Collections.Generic;
using Dvl_Sql.Extensions;
using System.Data;

namespace Dvl_Sql.Models
{
    public class DvlSqlType
    {
        public string Name { get; }

        public SqlDbType SqlDbType { get; }

        public int? Size { get; }

        public byte? Precision { get; }

        public byte? Scale { get;  }

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

        public DvlSqlType(TValue value, DvlSqlType dvlSqlType) : base(dvlSqlType.Name, dvlSqlType.SqlDbType,
            dvlSqlType.Size, dvlSqlType.Precision, dvlSqlType.Scale) => this.Value = value;

        public DvlSqlType(string name, TValue value, int? size = null, byte? precision = null, byte? scale = null) :
            base(name, SqlType.DefaultMap(value), size, precision, scale) =>
            this.Value = value;

        public DvlSqlType(string name, TValue value, SqlDbType dbType, int? size = null, byte? precision = null,
            byte? scale = null) : base(name, dbType, size, precision, scale) =>
            this.Value = value;

        public DvlSqlType(TValue value, SqlDbType dbType, int? size = null, byte? precision = null,
            byte? scale = null) : base(null, dbType, size, precision, scale) =>
            this.Value = value;

        public override bool Equals(object obj) => 
            obj is DvlSqlType<TValue> type && Equals(type);

        private bool Equals(DvlSqlType<TValue> other) =>
            other.Value.Equals(this.Value) &&
            base.Equals(other);

        public override int GetHashCode() => EqualityComparer<TValue>.Default.GetHashCode(Value) + base.GetHashCode();
    }

}
