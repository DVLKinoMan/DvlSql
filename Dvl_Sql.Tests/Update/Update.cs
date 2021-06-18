using System;
using System.Linq;
using System.Text.RegularExpressions;
using Dvl_Sql.Abstract;
using NUnit.Framework;

using static Dvl_Sql.ExpressionHelpers;
using static Dvl_Sql.SqlType;

namespace Dvl_Sql.Tests.Update
{
    [TestFixture]
    public class Update
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");
        
        [Test]
        public void TestMethod1()
        {
            var actualUpdate = this._sql.Update("dbo.Words")
                .Set(Money("money", new decimal(2.11)))
                .ToString();

            string expectedUpdate = Regex.Escape(
                $"UPDATE dbo.Words{Environment.NewLine}SET money = @money");

            Assert.That(Regex.Escape(actualUpdate), Is.EqualTo(expectedUpdate));
        }

        [Test]
        public void TestMethod2()
        {
            var actualUpdate = this._sql.Update("dbo.Words")
                .Set(Money("money", new decimal(3.11)))
                .Where(ConstantExpCol("Amount") == "@amount",
                    Param("@amount", Decimal(new decimal(42))))
                .ToString();
            
            string expectedUpdate = Regex.Escape(
                string.Format("UPDATE dbo.Words{0}" + 
                              "SET money = @money{0}" +
                              "WHERE Amount = @amount",
                    Environment.NewLine));

            Assert.That(Regex.Escape(actualUpdate), Is.EqualTo(expectedUpdate));
        }

        [Test]
        public void TestMethod3()
        {
            var actualUpdate = this._sql.Update("dbo.Words")
                .Set(Money("money", new decimal(3.11)))
                .Set(Bit("isSome", true))
                .Set(Float("floatNumber", 1.222))
                .Set(BigInt("bigint", 1111111111))
                .Set(Xml("xml", "<xml></xml>"))
                .Set(DateTime("Date", System.DateTime.Now))
                .Where(ConstantExpCol("Amount") == "@amount",
                    Param("@amount", Decimal(new decimal(42))))
                .ToString();
            
            string expectedUpdate = Regex.Escape(
                string.Format("UPDATE dbo.Words{0}" +
                              "SET money = @money, isSome = @isSome, floatNumber = @floatNumber, bigint = @bigint, xml = @xml, Date = @Date{0}" +
                              "WHERE Amount = @amount", 
                    Environment.NewLine));

            Assert.That(Regex.Escape(actualUpdate), Is.EqualTo(expectedUpdate));
        }

        [Test]
        public void TestMethod4()
        {
            string folderPath = "some/path";
            string exactValue = $"\'{folderPath}\' + {ConvertExp("nvarchar(260)", "Id")}";

            var someIds = new Guid[] {new Guid(), new Guid(), new Guid()};
            var actualUpdate = this._sql.Update("dbo.Words")
                .Set(NVarCharWithExactValue("RelativePath", exactValue,260))
                .Where(InExp("Id", someIds.Select(id=>ConstantExp(id)).ToArray()))
                .ToString();

            string expectedUpdate = Regex.Escape(
                string.Format("UPDATE dbo.Words{0}" +
                              "SET RelativePath = " + exactValue + "{0}" +
                              "WHERE Id IN ( '" + string.Join("', '", someIds) + "' )",
                    Environment.NewLine));

            Assert.That(Regex.Escape(actualUpdate), Is.EqualTo(expectedUpdate));
        }
    }
}