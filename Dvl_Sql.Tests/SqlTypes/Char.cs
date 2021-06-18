using System.Data;
using DvlSql.Models;
using NUnit.Framework;
using static DvlSql.SqlType;

namespace DvlSql.Tests.SqlTypes
{
    [TestFixture]
    public class Char
    {
        [Test]
        [TestCase("SomeName", "SomeValue", 20)]
        public void CharWithNameValueAndSize(string name, string value, int size)
        {
            var type = Char(name, value, size);
            Assert.That(type.Equals(new DvlSqlType<string>(name, value, SqlDbType.Char, size)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeValue", 20)]
        public void CharWithValue(string value, int size)
        {
            var type = Char(value, size);
            Assert.That(type.Equals(new DvlSqlType<string>(value, SqlDbType.Char, size)), Is.EqualTo(true));
        }

        [Test]
        [TestCase(20)]
        public void CharTypeWithSize(int size)
        {
            var type = CharType(size);
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.Char, size)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1", 20)]
        [TestCase("SomeName2", 20)]
        public void CharTypeWithName(string name, int size)
        {
            var type = CharType(name, size);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.Char, size)), Is.EqualTo(true));
        }
    }
}