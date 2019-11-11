using System.Data;
using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Concrete;
using DVL_SQL_Test1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static DVL_SQL_Test1.Helpers.DvlSqlDataReaderHelpers;
using static DVL_SQL_Test1.Helpers.DvlSqlHelpers;

namespace DVL_SQL_Test1.Tests
{
    [TestClass]
    public class DvlSqlProcedureTester
    {
        private readonly IDvlSql _sql =
            new DvlSql(
                @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

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
