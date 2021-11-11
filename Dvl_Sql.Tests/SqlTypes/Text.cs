using System.Data;

using NUnit.Framework;
using static DvlSql.SqlType;

namespace DvlSql.Tests.SqlTypes
{
    [TestFixture]
    public class Text
    {
        [Test]
        [TestCase("SomeName", "SomeValue")]
        public void TextWithNameValueAndSize(string name, string value)
        {
            var type = Text(name, value);
            Assert.That(type.Equals(new DvlSqlType<string>(name, value, SqlDbType.Text)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeValue")]
        public void TextWithValue(string value)
        {
            var type = Text(value);
            Assert.That(type.Equals(new DvlSqlType<string>(value, SqlDbType.Text)), Is.EqualTo(true));
        }

        [Test]
        public void TextTypeWithSize()
        {
            var type = TextType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.Text)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void TextTypeWithName(string name)
        {
            var type = TextType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.Text)), Is.EqualTo(true));
        }
    }
}