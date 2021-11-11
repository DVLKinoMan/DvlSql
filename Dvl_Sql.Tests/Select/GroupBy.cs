using System;
using System.Text.RegularExpressions;
using DvlSql.SqlServer;
using NUnit.Framework;
using static DvlSql.ExpressionHelpers;
using static DvlSql.SqlType;

namespace DvlSql.Tests.Select
{
    [TestFixture]
    public class 
        GroupBy
    {
        private readonly IDvlSql _sql =
            new DvlSqlMs(
                StaticConnectionStrings.ConnectionStringForTest);

        private string TableName = "dbo.Words";

        [Test]
        [TestCase("Id", "name")]
        public void GroupByWithSelectParams(params string[] groupParams)
        {
            var actualSelect = this._sql.From(TableName)
                .GroupBy(groupParams)
                .Select(groupParams)
                .ToString();

            string @params = string.Join(", ", groupParams);
            var expectedSelect = Regex.Escape($"SELECT {@params} FROM {TableName}{Environment.NewLine}" +
                                              $"GROUP BY {@params}");

            Console.WriteLine($"Actual: {actualSelect}");

            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
        
        [Test]
        [TestCase("Id", "Name")]
        public void GroupByWithHaving(params string[] groupParams)
        {
            var actualSelect = this._sql.From(TableName)
                .GroupBy(groupParams)
                .Having(ConstantExpCol(groupParams[0]) == 1)
                .Select(groupParams)
                .ToString();

            string @params = string.Join(", ", groupParams);
            var expectedSelect = Regex.Escape($"SELECT {@params} FROM {TableName}{Environment.NewLine}" +
                                              $"GROUP BY {@params}{Environment.NewLine}" +
                                              $"HAVING {groupParams[0]} = {1}");

            Console.WriteLine($"Actual: {actualSelect}");

            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
        
        [Test]
        [TestCase("date", "Name")]
        public void GroupByWithHavingAndParams(params string[] groupParams)
        {
            var actualSelect = this._sql.From(TableName)
                .GroupBy(groupParams)
                .Having(ConstantExpCol(groupParams[0]) == "@date", Params(Param("@date", new DateTime(2019,11,11))) )
                .Select(groupParams)
                .ToString();

            string @params = string.Join(", ", groupParams);
            var expectedSelect = Regex.Escape($"SELECT {@params} FROM {TableName}{Environment.NewLine}" +
                                              $"GROUP BY {@params}{Environment.NewLine}" +
                                              $"HAVING {groupParams[0]} = @date");

            Console.WriteLine($"Actual: {actualSelect}");

            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
    }
}