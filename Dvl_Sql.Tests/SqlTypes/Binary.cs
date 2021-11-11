using System.Data;

using NUnit.Framework;
using static DvlSql.SqlType;

namespace DvlSql.Tests.SqlTypes
{
    [TestFixture]
    public class Binary
    {
        [Test]
        [TestCase("SomeName", new byte[] {1, 2, 3, 4, 5})]
        public void BinaryWithNameAndValue(string name, byte[] value)
        {
            var type = Binary(name, value);
            Assert.That(type.Equals(new DvlSqlType<byte[]>(name, value, SqlDbType.Binary)), Is.EqualTo(true));
        }

        [Test]
        [TestCase(new byte[]{1,2,3,4,4,4,3,211,2,122})]
        public void BinaryWithValue(byte[] value)
        {
            var type = Binary(value);
            Assert.That(type.Equals(new DvlSqlType<byte[]>(value, SqlDbType.Binary)), Is.EqualTo(true));
        }

        [Test]
        public void BinaryTypeTest()
        {
            var type = BinaryType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.Binary)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void BinaryTypeWithName(string name)
        {
            var type = BinaryType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.Binary)), Is.EqualTo(true));
        }
    }
}