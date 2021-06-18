using Dvl_Sql.Abstract;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;
using static Dvl_Sql.ExpressionHelpers;

namespace Dvl_Sql.Tests.Select
{
    [TestFixture]
    public class From
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(
                StaticConnectionStrings.ConnectionStringForTest);

        [Test]
        [TestCase("dbo.Words")]
        public void WithoutSelect(string tableName)
        {
            var from = this._sql.From(tableName);

            Assert.Throws<ArgumentNullException>(() =>
                @from.ToString()
            );
        }

        [Test]
        [TestCase("dbo.Words")]
        public void WithoutNoLock(string tableName)
        {
            var actualSelect1 = this._sql.From(tableName)
                .Select()
                .ToString();
            
            var actualSelect2 = this._sql.From(tableName, false)
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape($"SELECT * FROM {tableName}");
            Assert.Multiple(() =>
            {
                Assert.That(Regex.Escape(actualSelect1), Is.EqualTo(expectedSelect));
                Assert.That(Regex.Escape(actualSelect2), Is.EqualTo(expectedSelect));
            });
        }

        [Test]
        [TestCase("dbo.Words")]
        public void WithNoLock(string tableName)
        {
            var actualSelect = this._sql.From(tableName, true)
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape($"SELECT * FROM {tableName} WITH(NOLOCK)");
            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }

        [Test]
        [TestCase("dbo.Words")]
        public void WithInnerSelect(string tableName)
        {
            var fullSelect = FullSelectExp(SelectExp(FromExp("dbo.Words")));
            var asName = "W";
            var actualSelect = this._sql.From(fullSelect, asName)
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape($"SELECT * FROM (SELECT * FROM dbo.Words) AS {asName}");
            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
    }
}