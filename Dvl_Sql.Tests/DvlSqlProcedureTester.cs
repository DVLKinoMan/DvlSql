using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dvl_Sql.Abstract;
using Moq;
using NUnit.Framework;
using static Dvl_Sql.Extensions.DataReader;
using static Dvl_Sql.Extensions.SqlType;

using static Dvl_Sql.Tests.Result.Helpers;

namespace Dvl_Sql.Tests
{
    //todo: procedure tests
    [TestFixture]
    public class DvlSqlProcedureTester
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        [Test]
        public void TestMethod1<T>(Func<IDataReader, T> func, List<T> data, T expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<T>(readerMoq);
            var moq = CreateConnectionMock<T>(commandMoq);

            var res = this._sql.Procedure("someProc")
                .ExecuteAsync(AsList(r => (string) r["Text"]))
                .Result;
            
            // Assert.That(actual, Is.EqualTo(expected));
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
