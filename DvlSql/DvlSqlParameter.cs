using static System.Exts.Extensions;

namespace DvlSql
{
    public class DvlSqlParameter<TValue> : DvlSqlParameter
    {
        public bool ExactValue { get; init; }
        public TValue Value { get; init; } = default!;

        public DvlSqlParameter(string name, DvlSqlType type) : base(name, type)
        {
            if (type is DvlSqlType<TValue> dvlSqlTypeValue)
            {
                this.ExactValue = dvlSqlTypeValue.ExactValue;
                Value = dvlSqlTypeValue.Value;
            }
        }

        public DvlSqlParameter(DvlSqlType type) : base(type.Name, type)
        {
            if (type is DvlSqlType<TValue> dvlSqlTypeValue)
            {
                this.ExactValue = dvlSqlTypeValue.ExactValue;
                Value = dvlSqlTypeValue.Value;
            }
        }
    }

    public class DvlSqlOutputParameter : DvlSqlParameter
    {
        //public object Value => this.SqlParameter.Value;
        //todo check if this works
        public object Value { get; set; } = default!;

        public DvlSqlOutputParameter(string name, DvlSqlType type) : base(name, type)
        {
        }
    }

    public abstract class DvlSqlParameter
    {
        public string Name { get; init; }

        public DvlSqlType DvlSqlType { get; init; }

        protected DvlSqlParameter(string name, DvlSqlType dvlSqlType)
        {
            Name = name;//.WithAlpha();
            DvlSqlType = dvlSqlType;
        }
    }
}
