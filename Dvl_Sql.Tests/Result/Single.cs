using System;
using System.Collections.Generic;
using System.Data;
using DvlSql.SqlServer;
using DvlSql.Tests.Classes;
using NUnit.Framework;
using static DvlSql.Tests.Result.Helpers;

namespace DvlSql.Tests.Result
{
    [TestFixture]
    public class Single
    {
        private string TableName = "dbo.Words";

        #region Parameters

        private static readonly object[] ParametersWithFunc =
            new[]
            {
                new object[]
                {
                    (Func<IDataReader, int>) (r => (int) r[0] + 1),
                    new List<int>() {1}, 2
                },
                new object[]
                {
                    (Func<IDataReader, string>) (r => ((string) r[0]).Substring(0, 1)),
                    new List<string>() {"David"}, "D"
                },
                new object[]
                {
                    (Func<IDataReader, SomeClass>) (r =>
                    {
                        var someClass = (SomeClass) r[0];
                        return new SomeClass(someClass.SomeIntField + 1,
                            someClass.SomeStringField.Substring(0, 1));
                    }),
                    new List<SomeClass>()
                        {new SomeClass(1, "David")},
                    new SomeClass(2, "D")
                }
            };

        private static readonly object[] ParametersWithoutFunc =
            new[]
            {
                new object[]
                {
                    new List<int>() {1}, 1
                },
                new object[]
                {
                    new List<string>() {"David"}, "David"
                },
                new object[]
                {
                    new List<SomeClass>()
                        {new SomeClass(1, "David")},
                    new SomeClass(1, "David")
                }
            };

        private static readonly object[] ParametersWithoutFuncThrowingException =
            new[]
            {
                new object[] {new List<int>()},
                new object[] {new List<string>()},
                new object[] {new List<SomeClass>()},
                new object[] {new List<int>() {1, 2, 3, 4}},
                new object[] {new List<string>() {"David", "Lasha"}},
                new object[] {new List<SomeClass>() {new SomeClass(1, "David"), new SomeClass(2, "Lasha")}}
            };

        private static readonly object[] ParametersWithFuncThrowingException =
        {
            new object[]
            {
                (Func<IDataReader, int>) (r => (int) r[0] + 1),
                new List<int>()
            },
            new object[]
            {
                (Func<IDataReader, string>) (r => r[0].ToString().Substring(0, 1)),
                new List<string>()
            }
        };

        #endregion

        [Test]
        [TestCaseSource(nameof(ParametersWithoutFunc))]
        public void FirstWithoutFunc<T>(List<T> data, T expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<T>(readerMoq);
            var moq = CreateConnectionMock<T>(commandMoq);

            var actual = new DvlSqlMs(moq.Object)
                .From(TableName)
                .Select()
                .SingleAsync<T>()
                .Result;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCaseSource(nameof(ParametersWithoutFuncThrowingException))]
        public void FirstWithoutFuncThrowsException<T>(List<T> data)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<T>(readerMoq);
            var moq = CreateConnectionMock<T>(commandMoq);

            Assert.Throws(Is.InstanceOf(typeof(Exception)), () =>
            {
                var res = new DvlSqlMs(moq.Object)
                    .From(TableName)
                    .Select()
                    .SingleAsync<T>()
                    .Result;
            });
        }

        [Test]
        [TestCaseSource(nameof(ParametersWithFunc))]
        public void FirstWithFunc<T>(Func<IDataReader, T> func, List<T> data, T expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<T>(readerMoq);
            var moq = CreateConnectionMock<T>(commandMoq);

            var actual = new DvlSqlMs(moq.Object)
                .From(TableName)
                .Select()
                .SingleAsync(func)
                .Result;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCaseSource(nameof(ParametersWithFuncThrowingException))]
        public void FirstWithFuncThrowsException<T>(Func<IDataReader, T> func, List<T> data)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<T>(readerMoq);
            var moq = CreateConnectionMock<T>(commandMoq);

            Assert.Throws(Is.InstanceOf(typeof(Exception)), () =>
            {
                var res = new DvlSqlMs(moq.Object)
                    .From(TableName)
                    .Select()
                    .SingleAsync(func)
                    .Result;
            });
        }

        //todo: test with where filter somehow
    }
}