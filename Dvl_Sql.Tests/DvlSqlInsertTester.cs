using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;
using Dvl_Sql.Abstract;
using Dvl_Sql.Expressions;
using Dvl_Sql.Models;

using static Dvl_Sql.Extensions.Expressions.ExpressionHelpers;
using static Dvl_Sql.Extensions.Types.TypeHelpers;

namespace Dvl_Sql.Tests
{
    [TestClass]
    public class DvlSqlInsertTester
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        private static IEnumerable<string> Columns(params string[] cols) => cols;

        [TestMethod]
        public void TestMethod1()
        {
            // ReSharper disable once UnusedVariable
            var affectedRows = this._sql
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
                .ExecuteAsync().Result;

            //Assert.AreEqual(affectedRows, 2);
        }

        [TestMethod]
        public void TestMethod2()
        {
            // ReSharper disable once UnusedVariable
            var affectedRows = this._sql
                .InsertInto<(int, string)>("dbo.Words", 
                    DecimalType("Amount"),
                    NVarCharType("Text",100))
                .Values(
                    (42, "newVal1"),
                    (43, "newVal2"),
                    (44, "newVal3")
                )
                .ExecuteAsync().Result;

            //Assert.AreEqual(affectedRows, 3);
        }

        [TestMethod]
        public void TestMethod3()
        {
            // ReSharper disable once UnusedVariable
            var affectedRows = this._sql
                .InsertInto("dbo.Words", Columns("Amount", "Text"))
                .SelectStatement(FullSelectExp(SelectTopExp(FromExp("dbo.Words"), 2, "Amount", "Text"),
                        orderByExpression: OrderByExp(("Text", Ordering.ASC)),
                        sqlWhereExpression: WhereExp(ConstantExp("Amount") == ConstantExp("@amount"))),
                    new DvlSqlParameter<int>("amount", new DvlSqlType<int>("@amount", 42, SqlDbType.Decimal))
                )
                .ExecuteAsync().Result;

            //Assert.AreEqual(affectedRows, 2);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var dvlSql =
                IDvlSql.DefaultDvlSql(
                    "Server=docker2;Database=ManagerConfirmation;User Id=sa;Password=Pa$$w0rd;Application Name=B7.ManualConfirmation.Api;");
            //@"Data Source=docker2; Initial Catalog=ManualConfirmation; Connection Timeout=30;User Id = sa; Password = Pa$$w0rd; Application Name =  B7.ManualConfirmation.Api");

            var insert = @"INSERT INTO dbo.[User] (UserId, FirstName, LastName, MobileNumber, Status, DeviceId)
            VALUES (@userId, @firstName, @lastName, @mobileNumber, @status, @deviceId)";

            var rows = dvlSql.InsertInto<(int, string, string, string, int, int)>(
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
                .ExecuteAsync().Result;

            var k = dvlSql.DeleteFrom("[ManagerConfirmation].[confirmation].[Users]")
                .Where(ConstantExp("UserId") == ConstantExp(2))
                .ExecuteAsync().Result;

        }
    }
}
