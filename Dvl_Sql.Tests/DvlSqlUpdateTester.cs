﻿using System.Text.RegularExpressions;
using Dvl_Sql.Abstract;
using NUnit.Framework;
using static Dvl_Sql.Extensions.Expressions.ExpressionHelpers;
using static Dvl_Sql.Extensions.Types.TypeHelpers;

namespace Dvl_Sql.Tests
{
    [TestFixture]
    public class DvlSqlUpdateTester
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");
        
        private string GetWithoutEscapeCharacters(string s) => Regex.Replace(s, @"[^\r\n]", " ");
        
        [Test]
        public void TestMethod1()
        {
            var actualUpdate = this._sql.Update("dbo.Words")
                .Set(Money("money", new decimal(2.11)))
                // .ExecuteAsync().Result;
                .ToString();
            
            string expectedUpdate = GetWithoutEscapeCharacters(
                @"UPDATE dbo.Words
SET @money = @money");

            Assert.That(GetWithoutEscapeCharacters(actualUpdate), Is.EqualTo(expectedUpdate));
        }

        [Test]
        public void TestMethod2()
        {
            var actualUpdate = this._sql.Update("dbo.Words")
                .Set(Money("money", new decimal(3.11)))
                .Where(ConstantExp("Amount") == ConstantExp("@amount"),
                    Param("@amount", Decimal(new decimal(42))))
                // .ExecuteAsync().Result;
                .ToString();
            
            string expectedUpdate = GetWithoutEscapeCharacters(
                @"UPDATE dbo.Words
SET @money = @money
WHERE Amount = @amount ");

            Assert.That(GetWithoutEscapeCharacters(actualUpdate), Is.EqualTo(expectedUpdate));
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
                .Where(ConstantExp("Amount") == ConstantExp("@amount"),
                    Param("@amount", Decimal(new decimal(42))))
                // .ExecuteAsync().Result;
                .ToString();
            
            string expectedUpdate = GetWithoutEscapeCharacters(
                @"UPDATE dbo.Words
SET @money = @money, @isSome = @isSome, @floatNumber = @floatNumber, @bigint = @bigint, @xml = @xml, @Date = @Date
WHERE Amount = @amount ");

            Assert.That(GetWithoutEscapeCharacters(actualUpdate), Is.EqualTo(expectedUpdate));
        }
    }
}
