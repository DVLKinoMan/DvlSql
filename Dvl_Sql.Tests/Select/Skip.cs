using Dvl_Sql.Abstract;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace Dvl_Sql.Tests.Select
{
    [TestFixture]
    public class Skip
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(
                "test");

        private string TableName = "dbo.Words";

        [Test]
        [TestCase(3, null)]
        [TestCase(5, 4)]
        public void SkipInSelect(int offset, int? fetchNext)
        {
            var actualSelect = _sql.From(TableName)
                .Select()
                .Skip(offset, fetchNext)
                .ToString();

            Console.WriteLine(actualSelect);

            var expectedSelect = Regex.Escape($"SELECT * FROM {TableName}{Environment.NewLine}" +
                $"OFFSET {offset} ROWS" +
                (fetchNext != null
                    ? $" FETCH NEXT {fetchNext} ROWS ONLY"
                    : ""));

            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
    }
}
