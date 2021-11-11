using System.Data;

using NUnit.Framework;
using static DvlSql.SqlType;

namespace DvlSql.Tests.SqlTypes
{
    [TestFixture]
    public class Money
    {
        [Test]
        [TestCase("SomeName", 1.2)]
        public void MoneyWithNameAndValue(string name, decimal value)
        {
            var type = Money(name, value);
            Assert.That(type.Equals(new DvlSqlType<decimal>(name, value, SqlDbType.Money)), Is.EqualTo(true));
        }

        [Test]
        [TestCase(1.2)]
        public void MoneyWithValue(decimal value)
        {
            var type = Money(value);
            Assert.That(type.Equals(new DvlSqlType<decimal>(value, SqlDbType.Money)), Is.EqualTo(true));
        }

        [Test]
        public void MoneyTypeTest()
        {
            var type = MoneyType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.Money)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void MoneyTypeWithName(string name)
        {
            var type = MoneyType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.Money)), Is.EqualTo(true));
        }
    }
}