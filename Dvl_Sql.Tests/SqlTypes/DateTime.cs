using System.Data;
using Dvl_Sql.Models;
using NUnit.Framework;
using static Dvl_Sql.SqlType;
using SysDateTime = System.DateTime;

namespace Dvl_Sql.Tests.SqlTypes
{
    [TestFixture]
    public class DateTime
    {
        private static readonly object[][] ParamsWithNameAndValue = new[]
        {
            new object[] {"SomeName", new System.DateTime(1994, 11, 11)}
        };
        private static readonly object[][] ParamsWithValue = new[]
        {
            new object[] {new System.DateTime(1994, 11, 11)}
        };
        
        [Test]
        [TestCaseSource(nameof(ParamsWithNameAndValue))]
        public void DateTimeWithNameAndValue(string name, SysDateTime value)
        {
            var type = DateTime(name, value);
            Assert.That(type.Equals(new DvlSqlType<SysDateTime>(name, value, SqlDbType.DateTime)), Is.EqualTo(true));
        }

        [Test]
        [TestCaseSource(nameof(ParamsWithValue))]
        public void DateTimeWithValue(SysDateTime value)
        {
            var type = DateTime(value);
            Assert.That(type.Equals(new DvlSqlType<SysDateTime>(value, SqlDbType.DateTime)), Is.EqualTo(true));
        }

        [Test]
        [TestCase()]
        public void DateTimeTypeWithSize()
        {
            var type = DateTimeType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.DateTime)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void DateTimeTypeWithName(string name)
        {
            var type = DateTimeType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.DateTime)), Is.EqualTo(true));
        }
    }
}