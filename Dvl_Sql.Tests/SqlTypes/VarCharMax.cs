using System.Data;
using Dvl_Sql.Models;
using NUnit.Framework;
using static Dvl_Sql.Helpers.SqlType;

namespace Dvl_Sql.Tests.SqlTypes
{
    [TestFixture]
    public class VarCharMax
    {
        [Test]
        [TestCase("SomeName", "SomeValue")]
        public void VarCharMaxWithNameAndValue(string name, string value)
        {
            var type = VarCharMax(name, value);
            Assert.That(type.Equals(new DvlSqlType<string>(name, value, SqlDbType.VarChar, -1)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeValue")]
        public void VarCharMaxWithValue(string value)
        {
            var type = VarCharMax(value);
            Assert.That(type.Equals(new DvlSqlType<string>(value, SqlDbType.VarChar, -1)), Is.EqualTo(true));
        }

        [Test]
        public void VarCharMaxTypeWithSize()
        {
            var type = VarCharMaxType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.VarChar, -1)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void VarCharMaxTypeWithName(string name)
        {
            var type = VarCharMaxType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.VarChar, -1)), Is.EqualTo(true));
        }
    }
}