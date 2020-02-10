using System.Collections.Generic;
using System.Text.RegularExpressions;
using Dvl_Sql.Abstract;
using Dvl_Sql.Expressions;
using NUnit.Framework;
using static Dvl_Sql.Extensions.Expressions;
using static Dvl_Sql.Extensions.SqlType;

namespace Dvl_Sql.Tests
{
    [TestFixture]
    public class DvlSqlInsertTester
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        private static IEnumerable<string> Columns(params string[] cols) => cols;
        
        private string GetWithoutEscapeCharacters(string s) => Regex.Replace(s, @"[^\r\n]", " ");
        
        [Test]
        public void TestMethod1()
        {
            // ReSharper disable once UnusedVariable
            var actualInsert = this._sql
                .InsertInto("dbo.Words", Columns("Amount", "Text"))
                .SelectStatement(
                    FullSelectExp(
                        SelectTopExp(
                            FromExp("dbo.Words"), 2, "Amount", "Text"),
                        orderByExpression: OrderByExp(
                            ("Text", Ordering.ASC)
                        )
                    )
                )
                // .ExecuteAsync().Result;
                .ToString();
            
            //Assert.AreEqual(affectedRows, 2);
            
            string expectedInsert = GetWithoutEscapeCharacters(
                @" INSERT INTO dbo.Words ( Amount, Text ) SELECT TOP 2 Amount, Text FROM dbo.Words
ORDER BY Text ASC");

            Assert.That(GetWithoutEscapeCharacters(actualInsert), Is.EqualTo(expectedInsert));
        }

        [Test]
        public void TestMethod2()
        {
            // ReSharper disable once UnusedVariable
            var actualInsert = this._sql
                .InsertInto<int, string,int, string,int, string,int,string, int, string>("dbo.Words",
                    DecimalType("Amount"),
                    NVarCharType("Text", 100),
                    DecimalType("Amount"),
                    NVarCharType("Text", 100),
                    DecimalType("Amount"),
                    NVarCharType("Text", 100),
                    DecimalType("Amount"),
                    NVarCharType("Text", 100),
                    DecimalType("Amount"),
                    NVarCharType("Text", 100))
                .Values(
                    (43, "newVal2", 42, "newVal1", 42, "newVal1", 42, "newVal1", 42, "newVal1"),
                    (44, "newVal3", 42, "newVal1", 42, "newVal1", 42, "newVal1", 42, "newVal1"),
                    (42, "newVal1", 42, "newVal1", 42, "newVal1", 42, "newVal1", 42, "newVal1")
                )
                // .ExecuteAsync().Result;
                .ToString();
            
            //Assert.AreEqual(affectedRows, 3);
            string expectedInsert = GetWithoutEscapeCharacters(
                @"
INSERT INTO dbo.Words ( Amount, Text )
VALUES
( @Amount1, @Text1 ),
( @Amount2, @Text2 ),
( @Amount3, @Text3 )");

            Assert.That(GetWithoutEscapeCharacters(actualInsert), Is.EqualTo(expectedInsert));
        }

        [Test]
        public void TestMethod3()
        {
            // ReSharper disable once UnusedVariable
            var actualInsert = this._sql
                .InsertInto("dbo.Words", Columns("Amount", "Text"))
                .SelectStatement(FullSelectExp(SelectTopExp(FromExp("dbo.Words"), 2, "Amount", "Text"),
                        orderByExpression: OrderByExp(("Text", Ordering.ASC)),
                        sqlWhereExpression: WhereExp(ConstantExp("Amount") == ConstantExp("@amount"))),
                    Param("amount", Decimal(42))
                )
                // .ExecuteAsync().Result;
                .ToString();
            //Assert.AreEqual(affectedRows, 2);
            string expectedInsert = GetWithoutEscapeCharacters(
                @" INSERT INTO dbo.Words ( Amount, Text ) SELECT TOP 2 Amount, Text FROM dbo.Words WHERE Amount = @amount
ORDER BY Text ASC");

            Assert.That(GetWithoutEscapeCharacters(actualInsert), Is.EqualTo(expectedInsert));
        }

        [Test]
        public void TestMethod4()
        {
            var dvlSql =
                IDvlSql.DefaultDvlSql(
                    "Server=docker2;Database=ManagerConfirmation;User Id=sa;Password=Pa$$w0rd;Application Name=B7.ManualConfirmation.Api;");
            //@"Data Source=docker2; Initial Catalog=ManualConfirmation; Connection Timeout=30;User Id = sa; Password = Pa$$w0rd; Application Name =  B7.ManualConfirmation.Api");

            // var insert = @"INSERT INTO dbo.[User] (UserId, FirstName, LastName, MobileNumber, Status, DeviceId)
            // VALUES (@userId, @firstName, @lastName, @mobileNumber, @status, @deviceId)";

            var actualInsert = dvlSql.InsertInto<(int, string, string, string, int, int)>(
                    "[ManagerConfirmation].[confirmation].[Users]",
                    IntType("userId"),
                    NVarCharType("firstName", 100),
                    NVarCharType("lastName", 100),
                    NVarCharType("mobileNumber", 20),
                    TinyIntType("status"),
                    NVarCharType("deviceId", 100))
                .Values(
                    (2, "ass", "ass", "ass", 1, 1)
                )
                // .ExecuteAsync().Result;
                .ToString();
            
            // var k = dvlSql.DeleteFrom("[ManagerConfirmation].[confirmation].[Users]")
            //     .Where(ConstantExp("UserId") == ConstantExp(2))
            //     .ExecuteAsync().Result;
            
            string expectedInsert = GetWithoutEscapeCharacters(
                @"
INSERT INTO [ManagerConfirmation].[confirmation].[Users] ( userId, firstName, lastName, mobileNumber, status, deviceId )
VALUES
( @userId1, @firstName1, @lastName1, @mobileNumber1, @status1, @deviceId1 )");

            Assert.That(GetWithoutEscapeCharacters(actualInsert), Is.EqualTo(expectedInsert));

        }
    }
}
