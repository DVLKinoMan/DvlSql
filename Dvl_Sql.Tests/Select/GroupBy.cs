using System;
using System.Text.RegularExpressions;
using Dvl_Sql.Abstract;
using NUnit.Framework;
using static Dvl_Sql.Extensions.Expressions;
using static Dvl_Sql.Extensions.SqlType;

namespace Dvl_Sql.Tests.Select
{
    [TestFixture]
    public class 
        GroupBy
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(
                "test");

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
                .Having(ConstantExp(groupParams[0]) == 1)
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
                .Having(ConstantExp(groupParams[0]) == "@date", Params(Param("@date", new DateTime(2019,11,11))) )
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