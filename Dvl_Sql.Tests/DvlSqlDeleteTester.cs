using System;
using System.Text.RegularExpressions;
using Dvl_Sql.Abstract;
using NUnit.Framework;
using static Dvl_Sql.Extensions.Expressions;
using static Dvl_Sql.Extensions.SqlType;

namespace Dvl_Sql.Tests
{
    [TestFixture]
    public class DvlSqlDeleteTester
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        [Test]
        public void TestMethod1()
        {
            var actualDelete = this._sql.DeleteFrom("dbo.Words")
                .Where(ConstantExp("Text") == ConstantExp("@text"),
                    Params(
                        Param("@text", NVarCharMax("New Text"))
                    ))
                // .ExecuteAsync().Result;
                .ToString();
            
            string expectedDelete = Regex.Escape(
                $"DELETE FROM dbo.Words{Environment.NewLine}" +
                $"WHERE Text = @text ");

            Assert.That(Regex.Escape(actualDelete), Is.EqualTo(expectedDelete));
        }

        [Test]
        public void TestMethod2()
        {
            var actualDelete = this._sql.DeleteFrom("dbo.Words")
                // .ExecuteAsync().Result;
                .ToString();
            
            string expectedDelete = Regex.Escape(
                @"DELETE FROM dbo.Words ");

            Assert.That(Regex.Escape(actualDelete), Is.EqualTo(expectedDelete));
        }
    }
}
