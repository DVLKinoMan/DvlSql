using System.Data;
using Dvl_Sql.Models;
using NUnit.Framework;
using static Dvl_Sql.SqlType;

namespace Dvl_Sql.Tests.SqlTypes
{
    [TestFixture]
    public class Image
    {
        [Test]
        [TestCase("SomeName", new byte[] {1, 2, 3, 4, 5})]
        public void ImageWithNameAndValue(string name, byte[] value)
        {
            var type = Image(name, value);
            Assert.That(type.Equals(new DvlSqlType<byte[]>(name, value, SqlDbType.Image)), Is.EqualTo(true));
        }

        [Test]
        [TestCase(new byte[]{1,2,3,4,4,4,3,211,2,122})]
        public void ImageWithValue(byte[] value)
        {
            var type = Image(value);
            Assert.That(type.Equals(new DvlSqlType<byte[]>(value, SqlDbType.Image)), Is.EqualTo(true));
        }

        [Test]
        public void ImageTypeTest()
        {
            var type = ImageType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.Image)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void ImageTypeWithName(string name)
        {
            var type = ImageType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.Image)), Is.EqualTo(true));
        }
        
    }
}