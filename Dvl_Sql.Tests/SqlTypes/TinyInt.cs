using System.Data;

using NUnit.Framework;
using static DvlSql.Extensions.SqlType;

namespace DvlSql.Tests.SqlTypes
{
    [TestFixture]
    public class TinyInt
    {
        [Test]
        [TestCase("SomeName", 1)]
        public void TinyIntWithNameAndValue(string name, byte value)
        {
            var type = TinyInt(name, value);
            Assert.That(type.Equals(new DvlSqlType<byte>(name, value, SqlDbType.TinyInt)), Is.EqualTo(true));
        }

        [Test]
        [TestCase(1)]
        public void TinyIntWithValue(byte value)
        {
            var type = TinyInt(value);
            Assert.That(type.Equals(new DvlSqlType<byte>(value, SqlDbType.TinyInt)), Is.EqualTo(true));
        }

        [Test]
        public void TinyIntTypeTest()
        {
            var type = TinyIntType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.TinyInt)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void TinyIntTypeWithName(string name)
        {
            var type = TinyIntType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.TinyInt)), Is.EqualTo(true));
        }        
    }
}