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
    public class SingleOrDefault
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
                    (Func<IDataReader, string>) (r => ((string) r[0]).Substring(0, 1)),
                    new List<string>() {"David", "Lasha"}, default(string)
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
                        {new SomeClass(1, "David"), new SomeClass(2, "Lasga")},
                    default(SomeClass)
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
                    new List<string>() {"David", "Lasha"}, default(string)
                },
                new object[]
                {
                    new List<SomeClass>()
                        {new SomeClass(1, "David")},
                    new SomeClass(1, "David")
                },
                new object[]
                {
                    new List<SomeClass>()
                        {new SomeClass(1, "David"), new SomeClass(2, "Lasha")},
                    default(SomeClass)
                }
            };

        #endregion

        [Test]
        [TestCaseSource(nameof(ParametersWithoutFunc))]
        public void SingleOrDefaultWithoutFunc<T>(List<T> data, T expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<T>(readerMoq);
            var moq = CreateConnectionMock<T>(commandMoq);

            var actual = new DvlSqlMs(moq.Object)
                .From(TableName)
                .Select()
                .SingleOrDefaultAsync<T>()
                .Result;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCaseSource(nameof(ParametersWithFunc))]
        public void SingleOrDefaultWithFunc<T>(Func<IDataReader, T> func, List<T> data, T expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<T>(readerMoq);
            var moq = CreateConnectionMock<T>(commandMoq);

            var actual = new DvlSqlMs(moq.Object)
                .From(TableName)
                .Select()
                .SingleOrDefaultAsync(func)
                .Result;

            Assert.That(actual, Is.EqualTo(expected));
        }
        
    }
}