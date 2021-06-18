using System;

namespace DvlSql.Tests.Classes
{
    public class SomeClass : IEquatable<SomeClass>
    {
        public readonly int SomeIntField;
        public readonly string SomeStringField;

        public SomeClass(int f1, string f2) =>
            (SomeIntField, SomeStringField) = (f1, f2);

        public bool Equals(SomeClass other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return SomeIntField == other.SomeIntField && SomeStringField == other.SomeStringField;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SomeClass) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SomeIntField, SomeStringField);
        }
    }
}