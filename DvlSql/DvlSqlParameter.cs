using System;

namespace DvlSql;

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

    public DvlSqlParameter(DvlSqlType type) : base(type.Name??throw new ArgumentNullException(nameof(type)), type)
    {
        if (type is DvlSqlType<TValue> dvlSqlTypeValue)
        {
            this.ExactValue = dvlSqlTypeValue.ExactValue;
            Value = dvlSqlTypeValue.Value;
        }
    }
}

public class DvlSqlOutputParameter(string name, DvlSqlType type) : DvlSqlParameter(name, type)
{
    //public object Value => this.SqlParameter.Value;
    //todo check if this works
    public object Value { get; set; } = default!;
}

public abstract class DvlSqlParameter(string name, DvlSqlType dvlSqlType)
{
    public string Name { get; init; } = name;//.WithAlpha();

    public DvlSqlType DvlSqlType { get; init; } = dvlSqlType;
}
