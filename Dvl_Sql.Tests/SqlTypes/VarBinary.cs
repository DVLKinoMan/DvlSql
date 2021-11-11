using System.Data;

using NUnit.Framework;
using static DvlSql.Extensions.SqlType;

namespace DvlSql.Tests.SqlTypes
{
    [TestFixture]
    public class VarBinary
    {
        [Test]
        [TestCase("SomeName", new byte[] {1, 2, 3, 4, 5})]
        public void VarBinaryWithNameAndValue(string name, byte[] value)
        {
            var type = VarBinary(name, value);
            Assert.That(type.Equals(new DvlSqlType<byte[]>(name, value, SqlDbType.VarBinary)), Is.EqualTo(true));
        }

        [Test]
        [TestCase(new byte[]{1,2,3,4,4,4,3,211,2,122})]
        public void VarBinaryWithValue(byte[] value)
        {
            var type = VarBinary(value);
            Assert.That(type.Equals(new DvlSqlType<byte[]>(value, SqlDbType.VarBinary)), Is.EqualTo(true));
        }

        [Test]
        public void VarBinaryTypeTest()
        {
            var type = VarBinaryType();
            Assert.That(type.Equals(new DvlSqlType(SqlDbType.VarBinary)), Is.EqualTo(true));
        }

        [Test]
        [TestCase("SomeName1")]
        [TestCase("SomeName2")]
        public void VarBinaryTypeWithName(string name)
        {
            var type = VarBinaryType(name);
            Assert.That(type.Equals(new DvlSqlType(name, SqlDbType.VarBinary)), Is.EqualTo(true));
        }
    }
}