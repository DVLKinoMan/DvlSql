using System.Data;
using Dvl_Sql.Models;
using NUnit.Framework;
using static Dvl_Sql.Extensions.SqlType;

namespace Dvl_Sql.Tests.SqlTypes
{
    [TestFixture]
    public class Int
    {
        [Test]
        [TestCase("SomeName", 1)]
        public void IntWithNameAndValue(string name, int value)
        {
            var type = Int(name, value);
            Assert.That(type.Equals(new DvlSqlType<int>(name, value, SqlDbType.Int)), Is.EqualTo(true));
        }

        [Test]
        [TestCase(1)]
        public void IntWithValue(int value)
        {
            var type = Int(value);
            Assert.That(type.Equals(new DvlSqlType<int>(value, SqlDbType.Int)), Is.EqualTo(true));
        }

        [Test]
        public void IntTypeTest()
        {
            var type = IntType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.Int)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void IntTypeWithName(string name)
        {
            var type = IntType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.Int)), Is.EqualTo(true));
        }
    }
}