using Dvl_Sql.Abstract;
using NUnit.Framework;
using static Dvl_Sql.Extensions.DataReader;
using static Dvl_Sql.Extensions.SqlType;

namespace Dvl_Sql.Tests
{
    //todo: procedure tests
    [TestFixture]
    public class DvlSqlProcedureTester
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        [Test]
        public void TestMethod1()
        {
            var res = this._sql.Procedure("someProc")
                .ExecuteAsync(AsList(r => (string) r["Text"]))
                .Result;
        }

        [Test]
        public void TestMethod2()
        {
            var outputParam = OutputParam("count", IntType());
            var res = this._sql.Procedure("SomeProc2",
                    Param("amount", 42),
                    outputParam)
                .ExecuteAsync(AsList(r => (string) r["Text"])).Result;

            var obj = outputParam.Value;
        }
    }
}
