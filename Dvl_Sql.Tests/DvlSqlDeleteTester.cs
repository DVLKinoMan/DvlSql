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

        private string GetWithoutEscapeCharacters(string s) => Regex.Replace(s, @"[^\r\n]", " ");
        
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
            
            string expectedDelete = GetWithoutEscapeCharacters(
                @"DELETE FROM dbo.Words
WHERE Text = @text ");

            Assert.That(GetWithoutEscapeCharacters(actualDelete), Is.EqualTo(expectedDelete));
        }

        [Test]
        public void TestMethod2()
        {
            var actualDelete = this._sql.DeleteFrom("dbo.Words")
                // .ExecuteAsync().Result;
                .ToString();
            
            string expectedDelete = GetWithoutEscapeCharacters(
                @"DELETE FROM dbo.Words ");

            Assert.That(GetWithoutEscapeCharacters(actualDelete), Is.EqualTo(expectedDelete));
        }
    }
}
