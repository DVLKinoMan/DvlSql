using System.Data;

using NUnit.Framework;
using static DvlSql.Extensions.SqlType;

namespace DvlSql.Tests.SqlTypes
{
    [TestFixture]
    public class NVarChar
    {
        [Test]
        [TestCase("SomeName", "SomeValue", 20)]
        public void NVarCharWithNameValueAndSize(string name, string value, int size)
        {
            var type = NVarChar(name, value, size);
            Assert.That(type.Equals(new DvlSqlType<string>(name, value, SqlDbType.NVarChar, size)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeValue", 20)]
        public void NVarCharWithValue(string value, int size)
        {
            var type = NVarChar(value, size);
            Assert.That(type.Equals(new DvlSqlType<string>(value, SqlDbType.NVarChar, size)), Is.EqualTo(true));
        }

        [Test]
        [TestCase(20)]
        public void NVarCharTypeWithSize(int size)
        {
            var type = NVarCharType(size);
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.NVarChar, size)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1", 20)]
        [TestCase("SomeName2", 20)]
        public void NVarCharTypeWithName(string name, int size)
        {
            var type = NVarCharType(name, size);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.NVarChar, size)), Is.EqualTo(true));
        }
    }
}