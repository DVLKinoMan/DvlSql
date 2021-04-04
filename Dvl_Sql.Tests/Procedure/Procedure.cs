using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Dvl_Sql.Abstract;
using NUnit.Framework;
using static Dvl_Sql.Helpers.DataReader;
using static Dvl_Sql.Helpers.SqlType;
using static Dvl_Sql.Tests.Result.Helpers;

namespace Dvl_Sql.Tests.Procedure
{
    [TestFixture]
    public class Procedure
    {
        private static readonly object[] ParametersWithFunc =
            new[]
            {
                new object[]
                {
                    (Func<IDataReader, string>) (r => (string)r[0]),
                    (Func<Func<IDataReader, string>, Func<IDataReader, List<string>>>) AsList,
                    new List<string>() {"first", "second", "third"},
                    new List<string>() {"first", "second", "third"}
                },
                new object[]
                {
                    (Func<IDataReader, string>) (r => (string)r[0]),
                    (Func<Func<IDataReader, string>, Func<IDataReader, string>>) First,
                    new List<string>() {"first", "second", "third"},
                    "first"
                }
            };

        [Test]
        [TestCaseSource(nameof(ParametersWithFunc))]
        public void TestMethod1<T, TResult>(Func<IDataReader, T> readerFunc,
            Func<Func<IDataReader, T>, Func<IDataReader, TResult>> func, List<T> data, TResult expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<TResult>(readerMoq);
            var moq = CreateConnectionMock<TResult>(commandMoq, CommandType.StoredProcedure);

            var actual = IDvlSql.DefaultDvlSql(moq.Object).Procedure("someProc")
                .ExecuteAsync(func(readerFunc))
                .Result;

            if (actual is { } && expected is IEnumerable enumerable)
                Assert.That(actual, Is.EquivalentTo(enumerable));
            else Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCaseSource(nameof(ParametersWithFunc))]
        public void TestMethod2<T, TResult>(Func<IDataReader, T> readerFunc,
            Func<Func<IDataReader, T>, Func<IDataReader, TResult>> func, List<T> data, TResult expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<TResult>(readerMoq);
            var moq = CreateConnectionMock<TResult>(commandMoq, CommandType.StoredProcedure);

            var outputParam = OutputParam("count", IntType());
            var actual = IDvlSql.DefaultDvlSql(moq.Object).Procedure("SomeProc2",
                    Param("amount", 42),
                    outputParam)
                .ExecuteAsync(func(readerFunc)).Result;

            if (actual is { } && expected is IEnumerable enumerable)
                Assert.That(actual, Is.EquivalentTo(enumerable));
            else Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(3)]
        [TestCase(4)]
        public void TestMethod3(int expected)
        {
            var commandMoq = CreateSqlCommandMock(expected);
            var moq = CreateConnectionMock<int>(commandMoq, CommandType.StoredProcedure);

            var actual = IDvlSql.DefaultDvlSql(moq.Object)
                .Procedure("SomeProc2")
                .ExecuteAsync().Result;

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}