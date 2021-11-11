using System.Data;

using NUnit.Framework;
using static DvlSql.Extensions.SqlType;

namespace DvlSql.Tests.SqlTypes
{
    [TestFixture]
    public class VarChar
    {
        [Test]
        [TestCase("SomeName", "SomeValue", 20)]
        public void VarCharWithNameValueAndSize(string name, string value, int size)
        {
            var type = VarChar(name, value, size);
            Assert.That(type.Equals(new DvlSqlType<string>(name, value, SqlDbType.VarChar, size)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeValue", 20)]
        public void VarCharWithValue(string value, int size)
        {
            var type = VarChar(value, size);
            Assert.That(type.Equals(new DvlSqlType<string>(value, SqlDbType.VarChar, size)), Is.EqualTo(true));
        }

        [Test]
        [TestCase(20)]
        public void VarCharTypeWithSize(int size)
        {
            var type = VarCharType(size);
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.VarChar, size)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1", 20)]
        [TestCase("SomeName2", 20)]
        public void VarCharTypeWithName(string name, int size)
        {
            var type = VarCharType(name, size);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.VarChar, size)), Is.EqualTo(true));
        }
    }
}