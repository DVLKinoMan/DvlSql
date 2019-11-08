using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static DVL_SQL_Test1.Helpers.DvlSqlDataReaderHelpers;

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
            var res = this._sql.ExecuteProcedureAsync("someProc", AsList(r => (string) r["Text"])).Result;
        }
    }
}
