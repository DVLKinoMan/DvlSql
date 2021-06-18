using System.Data;
using DvlSql.Models;
using NUnit.Framework;
using static DvlSql.SqlType;

namespace DvlSql.Tests.SqlTypes
{
    [TestFixture]
    public class NCharMax
    {
        [Test]
        [TestCase("SomeName", "SomeValue")]
        public void NCharWithNameAndValue(string name, string value)
        {
            var type = NCharMax(name, value);
            Assert.That(type.Equals(new DvlSqlType<string>(name, value, SqlDbType.NChar, -1)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeValue")]
        public void NCharWithValue(string value)
        {
            var type = NCharMax(value);
            Assert.That(type.Equals(new DvlSqlType<string>(value, SqlDbType.NChar, -1)), Is.EqualTo(true));
        }

        [Test]
        public void NCharTypeWithSize()
        {
            var type = NCharMaxType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.NChar, -1)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void NCharTypeWithName(string name)
        {
            var type = NCharMaxType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.NChar, -1)), Is.EqualTo(true));
        }
    }
}