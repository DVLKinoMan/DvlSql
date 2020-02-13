using System;
using System.Text.RegularExpressions;
using Dvl_Sql.Abstract;
using NUnit.Framework;

namespace Dvl_Sql.Tests.Select
{
    [TestFixture]
    public class Union
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(
                "test");

        [Test]
        public void Select_With_Union()
        {
            string table1 = "dbo.Words";
            string table2 = "dbo.Sentences";

            var actualSelect = this._sql
                .From(table1)
                .Select()
                .Union()
                .From(table2)
                .Select()
                .ToString();

            string expectedSelect = Regex.Escape(string.Format(
                "SELECT * FROM {1}{0}" +
                "UNION{0}" +
                "SELECT * FROM {2}",
                Environment.NewLine,
                table1,
                table2));

            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }

        [Test]
        public void Select_With_UnionAll()
        {
            string table1 = "dbo.Words";
            string table2 = "dbo.Sentences";

            var actualSelect = this._sql
                .From(table1)
                .Select()
                .UnionAll()
                .From(table2)
                .Select()
                .ToString();

            string expectedSelect = Regex.Escape(string.Format(
                "SELECT * FROM {1}{0}" +
                "UNION ALL{0}" +
                "SELECT * FROM {2}",
                Environment.NewLine,
                table1,
                table2));

            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }


        [Test]
        public void Select_With_UnionAndUnionAllCombinations()
        {
            string table1 = "dbo.Words";
            string table2 = "dbo.Sentences";

            var select = this._sql
                .From(table1)
                .Select();

            var firstUnion = select.Union()
                .From(table2)
                .Select();

            var firstUnionAll = select.UnionAll()
                .From(table2)
                .Select();

            var actualSelect1 = firstUnion.UnionAll()
                .From(table1)
                .Select()
                .ToString();

            var actualSelect2 = firstUnionAll.Union()
                .From(table1)
                .Select()
                .ToString();

            string expectedSelect1 = Regex.Escape(string.Format(
                "SELECT * FROM {1}{0}" +
                "UNION{0}" +
                "SELECT * FROM {2}{0}" +
                "UNION ALL{0}" +
                "SELECT * FROM {1}",
                Environment.NewLine,
                table1,
                table2));
            
            string expectedSelect2 = Regex.Escape(string.Format(
                "SELECT * FROM {1}{0}" +
                "UNION ALL{0}" +
                "SELECT * FROM {2}{0}" +
                "UNION{0}" +
                "SELECT * FROM {1}",
                Environment.NewLine,
                table1,
                table2));

            //todo: problem because it Union changes state
            // Assert.Multiple(() =>
            // {
            //     Assert.That(Regex.Escape(actualSelect1), Is.EqualTo(expectedSelect1));
            //     Assert.That(Regex.Escape(actualSelect2), Is.EqualTo(expectedSelect2));
            // });
        }
    }
}