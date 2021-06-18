using System.Data;
using Dvl_Sql.Models;
using NUnit.Framework;
using static Dvl_Sql.SqlType;

namespace Dvl_Sql.Tests.SqlTypes
{
    [TestFixture]
    public class Decimal
    {
        [Test]
        [TestCase("SomeName", 1.2)]
        public void DecimalWithNameAndValue(string name, decimal value)
        {
            var type = Decimal(name, value);
            Assert.That(type.Equals(new DvlSqlType<decimal>(name, value, SqlDbType.Decimal)), Is.EqualTo(true));
        }

        [Test]
        [TestCase(1.2)]
        public void DecimalWithValue(decimal value)
        {
            var type = Decimal(value);
            Assert.That(type.Equals(new DvlSqlType<decimal>(value, SqlDbType.Decimal)), Is.EqualTo(true));
        }

        [Test]
        public void DecimalTypeTest()
        {
            var type = DecimalType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.Decimal)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void DecimalTypeWithName(string name)
        {
            var type = DecimalType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.Decimal)), Is.EqualTo(true));
        }
    }
}