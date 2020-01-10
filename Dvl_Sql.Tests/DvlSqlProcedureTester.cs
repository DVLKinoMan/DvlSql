using System.Data;
using Dvl_Sql.Abstract;
using Dvl_Sql.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Dvl_Sql.Helpers.DvlSqlDataReaderHelpers;
using static Dvl_Sql.Helpers.DvlSqlHelpers;

namespace Dvl_Sql.Tests
{
    [TestClass]
    public class DvlSqlProcedureTester
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        [TestMethod]
        public void TestMethod1()
        {
            var res = this._sql.Procedure("someProc").ExecuteAsync(AsList(r => (string) r["Text"])).Result;
        }

        [TestMethod]
        public void TestMethod2()
        {
            var outputParam = OutputParam("count", new DvlSqlType(SqlDbType.Int));
            var res = this._sql.Procedure("SomeProc2",
                    Param("amount", 42),
                    outputParam)
                .ExecuteAsync(AsList(r => (string) r["Text"])).Result;

            var obj = outputParam.Value;
        }
    }
}
