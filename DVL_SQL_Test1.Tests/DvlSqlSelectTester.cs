using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Concrete;
using DVL_SQL_Test1.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using static DVL_SQL_Test1.Helpers.DvlSqlAggregateFunctionHelpers;
using static DVL_SQL_Test1.Helpers.DvlSqlExpressionHelpers;
using static DVL_SQL_Test1.Helpers.DvlSqlHelpers;

namespace DVL_SQL_Test1.Tests
{
    [TestClass]
    public class DvlSqlSelectTester
    {
        private readonly IDvlSql _sql =
            new DvlSql(
                "Data Source=SQL; Initial Catalog=BANK2000; Connection Timeout=30; User Id=b2000; Password=1234; Application Name = CoreApi");

        [TestMethod]
        public void TestMethod1()
        {
            var list = _sql
                .From(AsExp("nbe.BANK_DATA", "B1"), true)
                .Join(AsExp("nbe.BANK_DATA", "B2"), ComparisonExp(ConstantExp("B1.REC_ID"), SqlComparisonOperator.Equality, ConstantExp("B2.REC_ID")))
                .Where(
                    AndExp(
                        ComparisonExp(ConstantExp("B1.AMOUNT"), SqlComparisonOperator.Less, ConstantExp(35000)),
                        NotInExp("B1.REC_ID", SelectTopExp(FromExp("nbe.BANK_DATA"), 4, "REC_ID")),
                        NotLikeExp("B1.RESTRICT_CODE", "%dd%"),
                        ConstantExp("B1.ADD_DATE") > ConstantExp("@date")
                    //ComparisonExp(ConstantExp("B1.STATUS"), SqlComparisonOperator.Equality, ConstantExp(1))
                    ), Params(Param("@date", new DateTime(2012, 1, 1)))
                )
                .GroupBy("B1.AMOUNT")
                .Having(ComparisonExp(ConstantExp("Count(*)"), SqlComparisonOperator.GreaterOrEqual, ConstantExp("2")))
                //.Where(ComparisonExp(ConstantExp("STATUS"), SqlComparisonOperator.Equality, ConstantExp(1)))
                //.SelectTop(4,"B1.STATUS", "B1.AMOUNT", "B1.RESTRICT_CODE")
                //.Select("B1.STATUS", "B1.AMOUNT", "B1.RESTRICT_CODE")
                .Select("B1.AMOUNT", AsExp(CountExp(), "[CountExp]"))
                //.OrderBy("B1.AMOUNT")
                .OrderByDescending("[CountExp]", "AMOUNT")
                //.OrderBy()
                .ToListAsync(r =>
                        new
                        {
                            //Status = (byte)r["STATUS"],
                            Amount = (decimal)r["AMOUNT"],
                            //RestrictCode = (string)r["RESTRICT_CODE"]
                            Count = (int)r["CountExp"]
                        }
                ).Result;

            Assert.Equals(list.Count, 5);
        }
    }
}
