using System.Data;
using Dvl_Sql.Models;
using NUnit.Framework;
using static Dvl_Sql.Extensions.SqlType;

namespace Dvl_Sql.Tests.SqlTypes
{
    [TestFixture]
    public class CharMax
    {
        [Test]
        [TestCase("SomeName", "SomeValue")]
        public void CharMaxWithNameAndValue(string name, string value)
        {
            var type = CharMax(name, value);
            Assert.That(type.Equals(new DvlSqlType<string>(name, value, SqlDbType.Char, -1)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeValue")]
        public void CharMaxWithValue(string value)
        {
            var type = CharMax(value);
            Assert.That(type.Equals(new DvlSqlType<string>(value, SqlDbType.Char, -1)), Is.EqualTo(true));
        }

        [Test]
        public void CharMaxTypeWithSize()
        {
            var type = CharMaxType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.Char, -1)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void CharMaxTypeWithName(string name)
        {
            var type = CharMaxType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.Char, -1)), Is.EqualTo(true));
        }
    }
}