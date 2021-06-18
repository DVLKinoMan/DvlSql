using System;
using System.Text;
using System.Text.RegularExpressions;
using DvlSql.Abstract;
using NUnit.Framework;

namespace DvlSql.Tests.Select
{
    [TestFixture]
    public class OrderBy
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(
                StaticConnectionStrings.ConnectionStringForTest);

        private string TableName = "dbo.Words";

        [Test]
        [TestCase("Id", "Name", "Date")]
        [TestCase("Id", "test1")]
        public void OrderByAscending(params string[] fields)
        {
            var actualSelect = _sql.From(TableName)
                .Select()
                .OrderBy(fields)
                .ToString();

            Console.WriteLine(actualSelect);

            var expectedSelect = Regex.Escape($"SELECT * FROM {TableName}{Environment.NewLine}" +
                                              $"ORDER BY {string.Join(" ASC, ", fields) + " ASC"}");

            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }

        [Test]
        [TestCase("Id", "Name", "Date")]
        [TestCase("Id", "test1")]
        public void OrderByDescending(params string[] fields)
        {
            var actualSelect = _sql.From(TableName)
                .Select()
                .OrderByDescending(fields)
                .ToString();

            Console.WriteLine(actualSelect);

            var expectedSelect = Regex.Escape($"SELECT * FROM {TableName}{Environment.NewLine}" +
                                              $"ORDER BY {string.Join(" DESC, ", fields) + " DESC"}");

            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }

        private static readonly (string, bool)[][] FieldsForAscendingAndDescending = new[]
        {
            new (string, bool)[]
            {
                ("Id", true),
                ("Name", false)
            },
            new (string, bool)[]
            {
                ("Id", true),
                ("Name", false),
                ("LastName", false),
                ("Date", true),
                ("Time", false),
            },
        };

        [Test]
        [TestCaseSource(nameof(FieldsForAscendingAndDescending))]
        public void OrderByAscendingAndDescending(params (string field, bool IsAscending)[] fields)
        {
            var select = _sql.From(TableName)
                .Select();

            IOrderExecutable orderExecutable = select;
            foreach (var (field, isAscending) in fields)
                orderExecutable =
                    isAscending ? orderExecutable.OrderBy(field) : orderExecutable.OrderByDescending(field);

            var actualSelect = orderExecutable.ToString();
            Console.WriteLine(actualSelect);

            var builder = new StringBuilder();
            foreach (var (field, isAscending) in fields)
                builder.Append($"{field} {(isAscending ? "ASC, " : "DESC, ")}");

            if (fields.Length > 0)
                builder.Remove(builder.Length - 2, 2);

            var expectedSelect = Regex.Escape($"SELECT * FROM {TableName}{Environment.NewLine}" +
                                              $"ORDER BY {builder.ToString()}");

            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
    }
}