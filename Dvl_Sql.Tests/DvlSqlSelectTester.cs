using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Dvl_Sql.Abstract;
using static Dvl_Sql.Helpers.DvlSqlAggregateFunctionHelpers;
using static Dvl_Sql.Helpers.DvlSqlExpressionHelpers;
using static Dvl_Sql.Helpers.DvlSqlHelpers;

namespace Dvl_Sql.Tests
{
    [TestClass]
    public class DvlSqlSelectTester
    {
        private readonly IDvlSql _sql1 =
            IDvlSql.DefaultDvlSql(
                "Data Source=SQL; Initial Catalog=BANK2000; Connection Timeout=30; User Id=b2000; Password=1234; Application Name = CoreApi");

        private readonly IDvlSql _sql2 =
            IDvlSql.DefaultDvlSql(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        [TestMethod]
        public void TestMethod1()
        {
            var list = this._sql1
                .From(AsExp("nbe.BANK_DATA", "B1"), true)
                .Join(AsExp("nbe.BANK_DATA", "B2"), ConstantExp("B1.REC_ID") == ConstantExp("B2.REC_ID"))
                .Where(
                        ConstantExp("B1.AMOUNT") < ConstantExp(35000) &
                        !InExp("B1.REC_ID", SelectTopExp(FromExp("nbe.BANK_DATA"), 4, "REC_ID")) &
                        !!!LikeExp("B1.RESTRICT_CODE", "%dd%") &
                        ConstantExp("B1.ADD_DATE") > ConstantExp("@date")
                    , Params(Param("@date", new DateTime(2012, 1, 1)))
                )
                .GroupBy("B1.AMOUNT")
                .Having(ConstantExp("Count(*)") >= ConstantExp("2"))
                .Select("B1.AMOUNT", AsExp(CountExp(), "[CountExp]"))
                .OrderByDescending("[CountExp]", "AMOUNT")
                .ToListAsync(r =>
                        new
                        {
                            Amount = (decimal)r["AMOUNT"],
                            Count = (int)r["CountExp"]
                        }
                ).Result;

            Assert.AreEqual(list.Count, 5);
        }

        [TestMethod]
        public void TestMethod2()
        {
            int first = this._sql2
                .From("dbo.Words")
                .Where(IsNullExp(ConstantExp("Date")))
                .SelectTop(1)
                .OrderByDescending("Amount")
                .FirstOrDefaultAsync(r => (int) r["Amount"])
                .Result;
        }

        [TestMethod]
        public void TestMethod3()
        {
            int first = this._sql2
                .From("dbo.Words")
                .Where(IsNullExp(ConstantExp("Date")))
                .SelectTop(1, "Amount")
                .OrderByDescending("Amount")
                .FirstAsync(r => (int)r["Amount"])
                .Result;
        }

        [TestMethod]
        public void TestMethod4()
        {
            var first = this._sql2
                .From("dbo.Words")
                .Where(!IsNullExp(ConstantExp("Date")))
                .SelectTop(1, "Amount", "Date")
                .OrderByDescending("Amount")
                .FirstAsync(r => new {amount = (int) r["Amount"], date = (DateTime) r["Date"]});

            //Assert.ThrowsException<Exception>(first.Result);
        }
    }
}
