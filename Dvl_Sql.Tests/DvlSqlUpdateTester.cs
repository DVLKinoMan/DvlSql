using Dvl_Sql.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Dvl_Sql.Extensions.Expressions.ExpressionHelpers;
using static Dvl_Sql.Extensions.Types.TypeHelpers;

namespace Dvl_Sql.Tests
{
    [TestClass]
    public class DvlSqlUpdateTester
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        [TestMethod]
        public void TestMethod1()
        {
            var rows2 = this._sql.Update("dbo.Words")
                .Set(Money("money", new decimal(2.11)))
                .ExecuteAsync().Result;
        }

        [TestMethod]
        public void TestMethod2()
        {
            var rows2 = this._sql.Update("dbo.Words")
                .Set(Money("money", new decimal(3.11)))
                .Where(ConstantExp("Amount") == ConstantExp("@amount"),
                    Param("@amount", Decimal(new decimal(42))))
                .ExecuteAsync().Result;
        }

        [TestMethod]
        public void TestMethod3()
        {
            var rows2 = this._sql.Update("dbo.Words")
                .Set(Money("money", new decimal(3.11)))
                .Set(Bit("isSome",true))
                .Set(Float("floatNumber", 1.222))
                .Set(BigInt("bigint",1111111111))
                .Set(Xml("xml", "<xml></xml>"))
                .Set(DateTime("Date",System.DateTime.Now))
                .Where(ConstantExp("Amount") == ConstantExp("@amount"),
                    Param("@amount", Decimal(new decimal(42))))
                .ExecuteAsync().Result;
        }
    }
}
