using Dvl_Sql.Abstract;
using Dvl_Sql.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Dvl_Sql.Helpers.DvlSqlExpressionHelpers;
using static Dvl_Sql.Helpers.DvlSqlHelpers;
using static Dvl_Sql.Models.CustomDvlSqlType;

namespace Dvl_Sql.Tests
{
    [TestClass]
    public class DvlSqlUpdateTester
    {
        private readonly IDvlSql _sql =
            new DvlSql(
                @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        [TestMethod]
        public void TestMethod1()
        {
            var rows2 = this._sql.Update("dbo.Words")
                .Set(("money", Money(new decimal(2.11))))
                .ExecuteAsync().Result;
        }

        [TestMethod]
        public void TestMethod2()
        {
            var rows2 = this._sql.Update("dbo.Words")
                .Set(("money", Money(new decimal(3.11))))
                .Where(ConstantExp("Amount") == ConstantExp("@amount"),
                    Param("@amount", Decimal(new decimal(42))))
                .ExecuteAsync().Result;
        }


        [TestMethod]
        public void TestMethod3()
        {
            var rows2 = this._sql.Update("dbo.Words")
                .Set(("money", Money(new decimal(3.11))))
                .Set(("isSome", Bit(true)))
                .Set(("floatNumber", Float(1.222)))
                .Set(("bigint", BigInt(1111111111)))
                .Set(("xml", Xml("<xml></xml>")))
                .Set(("Date", DateTime(System.DateTime.Now)))
                .Where(ConstantExp("Amount") == ConstantExp("@amount"),
                    Param("@amount", Decimal(new decimal(42))))
                .ExecuteAsync().Result;
        }
    }
}
