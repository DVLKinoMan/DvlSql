using System;
using System.Text.RegularExpressions;
using Dvl_Sql.Abstract;
using NUnit.Framework;

namespace Dvl_Sql.Tests.Select
{
    [TestFixture]
    public class From
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(
                "test");

        private string TableName = "dbo.Words";
        
        [Test]
        public void WithoutSelect()
        {
            var from = this._sql.From(TableName);

            Assert.Throws(typeof(ArgumentNullException), () =>
            {
                var s = @from.ToString();
            });
        }

        [Test]
        public void WithoutNoLock()
        {
            var actualSelect1 = this._sql.From(TableName)
                .Select()
                .ToString();
            
            var actualSelect2 = this._sql.From(TableName, false)
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape($"SELECT * FROM {TableName}");
            Assert.Multiple(() =>
            {
                Assert.That(Regex.Escape(actualSelect1), Is.EqualTo(expectedSelect));
                Assert.That(Regex.Escape(actualSelect2), Is.EqualTo(expectedSelect));
            });
        }

        [Test]
        public void WithNoLock()
        {
            var actualSelect = this._sql.From(TableName, true)
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape($"SELECT * FROM {TableName} WITH(NOLOCK)");
            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
    }
}