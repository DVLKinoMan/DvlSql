using System.Data;
using Dvl_Sql.Models;
using NUnit.Framework;
using static Dvl_Sql.Helpers.SqlType;

namespace Dvl_Sql.Tests.SqlTypes
{
    public class NText
    {
        [Test]
        [TestCase("SomeName", "SomeValue")]
        public void NTextWithNameValueAndSize(string name, string value)
        {
            var type = NText(name, value);
            Assert.That(type.Equals(new DvlSqlType<string>(name, value, SqlDbType.NText)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeValue")]
        public void NTextWithValue(string value)
        {
            var type = NText(value);
            Assert.That(type.Equals(new DvlSqlType<string>(value, SqlDbType.NText)), Is.EqualTo(true));
        }

        [Test]
        public void NTextTypeWithSize()
        {
            var type = NTextType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.NText)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void NTextTypeWithName(string name)
        {
            var type = NTextType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.NText)), Is.EqualTo(true));
        }
    }
}