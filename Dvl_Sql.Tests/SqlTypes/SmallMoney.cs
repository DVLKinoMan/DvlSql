using System.Data;
using DvlSql.Models;
using NUnit.Framework;
using static DvlSql.SqlType;

namespace DvlSql.Tests.SqlTypes
{
    [TestFixture]
    public class SmallMoney
    {
        [Test]
        [TestCase("SomeName", 1.2)]
        public void SmallMoneyWithNameAndValue(string name, decimal value)
        {
            var type = SmallMoney(name, value);
            Assert.That(type.Equals(new DvlSqlType<decimal>(name, value, SqlDbType.SmallMoney)), Is.EqualTo(true));
        }

        [Test]
        [TestCase(1.2)]
        public void SmallMoneyWithValue(decimal value)
        {
            var type = SmallMoney(value);
            Assert.That(type.Equals(new DvlSqlType<decimal>(value, SqlDbType.SmallMoney)), Is.EqualTo(true));
        }

        [Test]
        public void SmallMoneyTypeTest()
        {
            var type = SmallMoneyType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.SmallMoney)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void SmallMoneyTypeWithName(string name)
        {
            var type = SmallMoneyType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.SmallMoney)), Is.EqualTo(true));
        }
    }
}