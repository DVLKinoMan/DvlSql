using System.Data;
using DvlSql.Models;
using NUnit.Framework;
using static DvlSql.SqlType;

namespace DvlSql.Tests.SqlTypes
{
    [TestFixture]
    public class Bit
    {
        [Test]
        [TestCase("SomeName", true)]
        [TestCase("SomeName", false)]
        public void BitWithNameAndValue(string name, bool value)
        {
            var type = Bit(name, value);
            Assert.That(type.Equals(new DvlSqlType<bool>(name, value, SqlDbType.Bit)), Is.EqualTo(true));
        }
        
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void BitWithValue(bool value)
        {
            var type = Bit(value);
            Assert.That(type.Equals(new DvlSqlType<bool>(value, SqlDbType.Bit)), Is.EqualTo(true));
        }
        
        [Test]
        public void BitTypeTest()
        {
            var type = BitType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.Bit)), Is.EqualTo(true));
        }
        
        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void BitTypeWithName(string name)
        {
            var type = BitType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.Bit)), Is.EqualTo(true));
        }
    }
}