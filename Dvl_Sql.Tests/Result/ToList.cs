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
    public class ToList
    {
        private string TableName = "dbo.Words";

        #region Parameters

        private static readonly object[] ParametersWithoutFunc =
            new[]
            {
                new object[]
                {
                    new List<int>() {1, 2, 3}, new List<int>() {1, 2, 3}
                },
                new object[]
                {
                    new List<string>() {"David", "Lasha", "SomeGuy"}, new List<string>() {"David", "Lasha", "SomeGuy"}
                },
                new object[]
                {
                    new List<SomeClass>()
                        {new SomeClass(1, "David"), new SomeClass(2, "Lasha"), new SomeClass(3, "SomeGuy")},
                    new List<SomeClass>()
                        {new SomeClass(1, "David"), new SomeClass(2, "Lasha"), new SomeClass(3, "SomeGuy")}
                }
            };

        private static readonly object[] ParametersWithFunc =
            new[]
            {
                new object[]
                {
                    (Func<IDataReader, int>) (r => (int) r[0] + 1),
                    new List<int>() {1, 2, 3}, new List<int>() {2, 3, 4}
                },
                new object[]
                {
                    (Func<IDataReader, string>) (r => ((string) r[0]).Substring(0, 1)),
                    new List<string>() {"David", "Lasha", "SomeGuy"}, new List<string>() {"D", "L", "S"}
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
                        {new SomeClass(1, "David"), new SomeClass(2, "Lasha"), new SomeClass(3, "SomeGuy")},
                    new List<SomeClass>()
                        {new SomeClass(2, "D"), new SomeClass(3, "L"), new SomeClass(4, "S")}
                }
            };

        #endregion

        [Test]
        [TestCaseSource(nameof(ParametersWithFunc))]
        public void TestToListWithFunc<T>(Func<IDataReader, T> func, List<T> data, List<T> expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<List<T>>(readerMoq);
            var moq = CreateConnectionMock<List<T>>(commandMoq);

            var list = new DvlSqlMs(moq.Object)
                .From(TableName)
                .Select()
                .ToListAsync(func)
                .Result;

            Assert.That(list, Is.EquivalentTo(expected));
        }

        [Test]
        [TestCaseSource(nameof(ParametersWithoutFunc))]
        public void TestToListWithoutFunc<T>(List<T> data, List<T> expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<List<T>>(readerMoq);
            var moq = CreateConnectionMock<List<T>>(commandMoq);

            var list = new DvlSqlMs(moq.Object)
                .From(TableName)
                .Select()
                .ToListAsync<T>()
                .Result;

            Assert.That(list, Is.EquivalentTo(expected));
        }
    }
}