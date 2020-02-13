using System;
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
                .ToString();
            
            string expectedInsert = Regex.Escape(
                $" INSERT INTO dbo.Words ( Amount, Text ) SELECT TOP 2 Amount, Text FROM dbo.Words{Environment.NewLine}ORDER BY Text ASC");

            Assert.That(Regex.Escape(actualInsert), Is.EqualTo(expectedInsert));
        }

        [Test]
        public void TestMethod2()
        {
            // ReSharper disable once UnusedVariable
            var actualInsert = this._sql
                .InsertInto<int, string>("dbo.Words",
                    DecimalType("Amount"),
                    NVarCharType("Text", 100))
                .Values(
                    (43, "newVal2"),
                    (44, "newVal3"),
                    (42, "newVal1")
                )
                .ToString();
            
            string expectedInsert = Regex.Escape(
                string.Format("{0}INSERT INTO dbo.Words ( Amount, Text ){0}" +
                              "VALUES{0}" +
                              "( @Amount1, @Text1 ),{0}" +
                              "( @Amount2, @Text2 ),{0}" +
                              "( @Amount3, @Text3 )", 
                    Environment.NewLine));

            Assert.That(Regex.Escape(actualInsert), Is.EqualTo(expectedInsert));
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
                .ToString();
            
            string expectedInsert = Regex.Escape(
                $" INSERT INTO dbo.Words ( Amount, Text ) SELECT TOP 2 Amount, Text FROM dbo.Words WHERE Amount = @amount{Environment.NewLine}ORDER BY Text ASC");

            Assert.That(Regex.Escape(actualInsert), Is.EqualTo(expectedInsert));
        }

        [Test]
        public void TestMethod4()
        {
            var dvlSql =
                IDvlSql.DefaultDvlSql(
                    "Server=docker2;Database=ManagerConfirmation;User Id=sa;Password=Pa$$w0rd;Application Name=B7.ManualConfirmation.Api;");

            var actualInsert = dvlSql.InsertInto<int, string, string, string, int, int>(
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
                .ToString();

            string expectedInsert = Regex.Escape(
                string.Format(
                    "{0}INSERT INTO [ManagerConfirmation].[confirmation].[Users] ( userId, firstName, lastName, mobileNumber, status, deviceId ){0}" +
                    "VALUES{0}" +
                    "( @userId1, @firstName1, @lastName1, @mobileNumber1, @status1, @deviceId1 )",
                    Environment.NewLine));

            Assert.That(Regex.Escape(actualInsert), Is.EqualTo(expectedInsert));

        }
    }
}
