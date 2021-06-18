using System.Data;
using DvlSql.Models;
using NUnit.Framework;
using static DvlSql.SqlType;

namespace DvlSql.Tests.SqlTypes
{
    [TestFixture]
    public class BigInt
    {
        [Test]
        [TestCase("SomeName", 1)]
        public void BigIntWithNameAndValue(string name, long value)
        {
            var type = BigInt(name, value);
            Assert.That(type.Equals(new DvlSqlType<long>(name, value, SqlDbType.BigInt)), Is.EqualTo(true));
        }

        [Test]
        [TestCase(1)]
        public void BigIntWithValue(long value)
        {
            var type = BigInt(value);
            Assert.That(type.Equals(new DvlSqlType<long>(value, SqlDbType.BigInt)), Is.EqualTo(true));
        }

        [Test]
        public void BigIntTypeTest()
        {
            var type = BigIntType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.BigInt)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void BigIntTypeWithName(string name)
        {
            var type = BigIntType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.BigInt)), Is.EqualTo(true));
        }        
    }
}