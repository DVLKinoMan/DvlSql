using System.Data;

using NUnit.Framework;
using static DvlSql.Extensions.SqlType;

namespace DvlSql.Tests.SqlTypes
{
    [TestFixture]
    public class Float
    {
        [Test]
        [TestCase("SomeName", 1.2)]
        public void FloatWithNameAndValue(string name, double value)
        {
            var type = Float(name, value);
            Assert.That(type.Equals(new DvlSqlType<double>(name, value, SqlDbType.Float)), Is.EqualTo(true));
        }

        [Test]
        [TestCase(1.2)]
        public void FloatWithValue(double value)
        {
            var type = Float(value);
            Assert.That(type.Equals(new DvlSqlType<double>(value, SqlDbType.Float)), Is.EqualTo(true));
        }

        [Test]
        public void FloatTypeTest()
        {
            var type = FloatType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.Float)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void FloatTypeWithName(string name)
        {
            var type = FloatType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.Float)), Is.EqualTo(true));
        }
    }
}