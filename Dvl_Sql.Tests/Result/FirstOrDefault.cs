using System;
using System.Collections.Generic;
using System.Data;
using Dvl_Sql.Abstract;
using Dvl_Sql.Tests.Classes;
using NUnit.Framework;
using static Dvl_Sql.Tests.Result.Helpers;

namespace Dvl_Sql.Tests.Result
{
    [TestFixture]
    public class FirstOrDefault
    {
        private string TableName = "dbo.Words";

        #region Parameters

        private static readonly object[] ParametersForFirstOrDefaultWithFunc =
            new[]
            {
                new object[]
                {
                    (Func<IDataReader, int>) (r => (int) r[0] + 1),
                    new List<int>() {1, 2, 3}, 2
                },
                new object[]
                {
                    (Func<IDataReader, string>) (r => ((string) r[0]).Substring(0, 1)),
                    new List<string>() {"David", "Lasha", "SomeGuy"}, "D"
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
                    new SomeClass(2, "D")
                },
                new object[]
                {
                    (Func<IDataReader, int>) (r => (int) r[0] + 1),
                    new List<int>(),
                    default(int)
                },
                new object[]
                {
                    (Func<IDataReader, string>) (r => r[0].ToString().Substring(0, 1)),
                    new List<string>(),
                    default(string)
                }
            };

        private static readonly object[] ParametersForFirstOrDefaultWithoutFunc =
            new[]
            {
                new object[] {new List<int>(), default(int)},
                new object[] {new List<string>(), default(string)},
                new object[] {new List<int>() {1, 2, 3, 4, 5, 15}, 1},
                new object[] {new List<int>() {15, 5, 4, 3, 2, 1}, 15}
            };

        #endregion

        [Test]
        [TestCaseSource(nameof(ParametersForFirstOrDefaultWithoutFunc))]
        public void FirstOrDefaultWithoutFunc<T>(List<T> data, T expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<T>(readerMoq);
            var moq = CreateConnectionMock<T>(commandMoq);

            var actual = IDvlSql.DefaultDvlSql(moq.Object)
                .From(TableName)
                .Select()
                .FirstOrDefaultAsync<T>()
                .Result;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCaseSource(nameof(ParametersForFirstOrDefaultWithFunc))]
        public void FirstOrDefaultWithFunc<T>(Func<IDataReader, T> func, List<T> data, T expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<T>(readerMoq);
            var moq = CreateConnectionMock<T>(commandMoq);

            var actual = IDvlSql.DefaultDvlSql(moq.Object)
                .From(TableName)
                .Select()
                .FirstOrDefaultAsync(func)
                .Result;

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}