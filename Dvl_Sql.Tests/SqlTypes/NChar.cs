using System.Data;
using Dvl_Sql.Models;
using NUnit.Framework;
using static Dvl_Sql.SqlType;

namespace Dvl_Sql.Tests.SqlTypes
{
    [TestFixture]
    public class NChar
    {
        [Test]
        [TestCase("SomeName", "SomeValue", 20)]
        public void NCharWithNameValueAndSize(string name, string value, int size)
        {
            var type = NChar(name, value, size);
            Assert.That(type.Equals(new DvlSqlType<string>(name, value, SqlDbType.NChar, size)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeValue", 20)]
        public void NCharWithValue(string value, int size)
        {
            var type = NChar(value, size);
            Assert.That(type.Equals(new DvlSqlType<string>(value, SqlDbType.NChar, size)), Is.EqualTo(true));
        }

        [Test]
        [TestCase(20)]
        public void NCharTypeWithSize(int size)
        {
            var type = NCharType(size);
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.NChar, size)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1", 20)]
        [TestCase("SomeName2", 20)]
        public void NCharTypeWithName(string name, int size)
        {
            var type = NCharType(name, size);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.NChar, size)), Is.EqualTo(true));
        }
    }
}