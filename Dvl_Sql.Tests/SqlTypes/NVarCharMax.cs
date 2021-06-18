using System.Data;
using Dvl_Sql.Models;
using NUnit.Framework;
using static Dvl_Sql.SqlType;

namespace Dvl_Sql.Tests.SqlTypes
{
    [TestFixture]
    public class NVarCharMax
    {
        [Test]
        [TestCase("SomeName", "SomeValue")]
        public void NVarCharMaxWithNameAndValue(string name, string value)
        {
            var type = NVarCharMax(name, value);
            Assert.That(type.Equals(new DvlSqlType<string>(name, value, SqlDbType.NVarChar, -1)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeValue")]
        public void NVarCharMaxWithValue(string value)
        {
            var type = NVarCharMax(value);
            Assert.That(type.Equals(new DvlSqlType<string>(value, SqlDbType.NVarChar, -1)), Is.EqualTo(true));
        }

        [Test]
        public void NVarCharMaxTypeWithSize()
        {
            var type = NVarCharMaxType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.NVarChar, -1)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void NVarCharMaxTypeWithName(string name)
        {
            var type = NVarCharMaxType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.NVarChar, -1)), Is.EqualTo(true));
        }
    }
}