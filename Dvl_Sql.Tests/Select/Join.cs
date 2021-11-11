using System;
using System.Text.RegularExpressions;
using DvlSql.SqlServer;
using NUnit.Framework;
using static DvlSql.Extensions.ExpressionHelpers;

namespace DvlSql.Tests.Select
{
    [TestFixture]
    public class Join
    {
        private readonly IDvlSql _sql =
            new DvlSqlMs(
                StaticConnectionStrings.ConnectionStringForTest);

        private string TableName = "dbo.Words";

        [Test]
        [TestCase("dbo.Sentences", "SentenceId", "Sentences.Id")]
        public void InnerJoin(string joinToTable, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            var actualSelect1 = _sql.From(TableName)
                .Join(joinToTable, ConstantExpCol(firstTableMatchingCol) == secondTableMatchingCol)
                .Select()
                .ToString();

            var actualSelect2 = _sql.From(TableName)
                .Join(joinToTable, firstTableMatchingCol, secondTableMatchingCol)
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape($"SELECT * FROM {TableName}{Environment.NewLine}" +
                                              $"INNER JOIN {joinToTable} ON {firstTableMatchingCol} = {secondTableMatchingCol}");

            Assert.Multiple(
                () =>
                {
                    Assert.That(Regex.Escape(actualSelect1), Is.EqualTo(expectedSelect));
                    Assert.That(Regex.Escape(actualSelect2), Is.EqualTo(expectedSelect));
                }
            );
        }

        [Test]
        [TestCase("dbo.Sentences", "SentenceId", "Sentences.Id")]
        public void LeftJoin(string joinToTable, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            var actualSelect1 = _sql.From(TableName)
                .LeftJoin(joinToTable, ConstantExpCol(firstTableMatchingCol) == secondTableMatchingCol)
                .Select()
                .ToString();

            var actualSelect2 = _sql.From(TableName)
                .LeftJoin(joinToTable, firstTableMatchingCol, secondTableMatchingCol)
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape($"SELECT * FROM {TableName}{Environment.NewLine}" +
                                              $"LEFT OUTER JOIN {joinToTable} ON {firstTableMatchingCol} = {secondTableMatchingCol}");

            Assert.Multiple(() =>
            {
                Assert.That(Regex.Escape(actualSelect1), Is.EqualTo(expectedSelect));
                Assert.That(Regex.Escape(actualSelect2), Is.EqualTo(expectedSelect));
            });
        }

        [Test]
        [TestCase("dbo.Sentences", "SentenceId", "Sentences.Id")]
        public void RightJoin(string joinToTable, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            var actualSelect1 = _sql.From(TableName)
                .RightJoin(joinToTable, ConstantExpCol(firstTableMatchingCol) == secondTableMatchingCol)
                .Select()
                .ToString();

            var actualSelect2 = _sql.From(TableName)
                .RightJoin(joinToTable, firstTableMatchingCol, secondTableMatchingCol)
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape($"SELECT * FROM {TableName}{Environment.NewLine}" +
                                              $"RIGHT OUTER JOIN {joinToTable} ON {firstTableMatchingCol} = {secondTableMatchingCol}");

            Assert.Multiple(() =>
            {
                Assert.That(Regex.Escape(actualSelect1), Is.EqualTo(expectedSelect));
                Assert.That(Regex.Escape(actualSelect2), Is.EqualTo(expectedSelect));
            });
        } 
        
        
        [Test]
        [TestCase("dbo.Sentences", "SentenceId", "Sentences.Id")]
        public void FullJoin(string joinToTable, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            var actualSelect1 = _sql.From(TableName)
                .FullJoin(joinToTable, ConstantExpCol(firstTableMatchingCol) == secondTableMatchingCol)
                .Select()
                .ToString();

            var actualSelect2 = _sql.From(TableName)
                .FullJoin(joinToTable, firstTableMatchingCol, secondTableMatchingCol)
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape($"SELECT * FROM {TableName}{Environment.NewLine}" +
                                              $"FULL OUTER JOIN {joinToTable} ON {firstTableMatchingCol} = {secondTableMatchingCol}");

            Assert.Multiple(() =>
            {
                Assert.That(Regex.Escape(actualSelect1), Is.EqualTo(expectedSelect));
                Assert.That(Regex.Escape(actualSelect2), Is.EqualTo(expectedSelect));
            });
        }
    }
}