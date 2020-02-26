using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dvl_Sql.Abstract;
using Moq;
using NUnit.Framework;
using static Dvl_Sql.Extensions.DataReader;
using static Dvl_Sql.Extensions.SqlType;
using static Dvl_Sql.Tests.Result.Helpers;

namespace Dvl_Sql.Tests
{
    //todo: procedure tests - Not working
    [TestFixture]
    public class DvlSqlProcedureTester
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(
                @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        private static readonly object[] ParametersWithFunc =
            new[]
            {
                new object[]
                {
                    (Func<IDataReader, string>) (r => r[0].ToString()),
                     (Func<Func<IDataReader, string>, Func<IDataReader, List<string>>>) AsList,
                    //(Func<IDataReader, List<string>>)(r => AsList((string)r[0])),
                    new List<string>() {"first", "second", "third"},
                    new List<string>() {"first", "second", "third"}
                },
                // new object[]
                // {
                //     (Func<IDataReader, string>) (r => ((string) r[0]).Substring(0, 1)),
                //     new List<string>() {"David", "Lasha", "SomeGuy"}, "D"
                // }
            };

        [Test]
        [TestCaseSource(nameof(ParametersWithFunc))]
        public void TestMethod1<T, TResult>(Func<IDataReader, T> readerFunc,
            Func<Func<IDataReader, T>, Func<IDataReader, TResult>> func, List<T> data, TResult expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<T>(readerMoq);
            var moq = CreateConnectionMock<T>(commandMoq, CommandType.StoredProcedure);

            var actual = IDvlSql.DefaultDvlSql(moq.Object).Procedure("someProc")
                .ExecuteAsync(func(readerFunc))
                .Result;

            if (actual is { } && expected is IEnumerable enumerable)
                Assert.That(actual, Is.EquivalentTo(enumerable));
            else Assert.That(actual, Is.EqualTo(expected));
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