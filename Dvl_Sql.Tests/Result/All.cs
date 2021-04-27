using Dvl_Sql.Abstract;
using Dvl_Sql.Tests.Classes;
using NUnit.Framework;
using System.Collections.Generic;
using static Dvl_Sql.Tests.Result.Helpers;
using static Dvl_Sql.Helpers.Expressions;

namespace Dvl_Sql.Tests.Result
{
    [TestFixture]
    public class All
    {
        private string TableName = "dbo.Words";

        #region Parameters

        private static readonly object[] ParametersWithoutWhere =
            new[]
            {
                new object[]
                {
                    new List<int>() {1, 2, 3}, true
                },
                new object[]
                {
                    new List<string>(), true
                },
                new object[]
                {
                    new List<SomeClass>()
                        {new SomeClass(1, "David"), new SomeClass(2, "Lasha"), new SomeClass(3, "SomeGuy")},
                    true
                },
                new object[]
                {
                    new List<SomeClass>()
                        {},
                    true
                }
            };

        private static readonly object[] ParametersWithWhereFor5Letters =
            new[]
            {
                new object[]
                {
                    new List<SomeClass>()
                        {new SomeClass(1, "David"), new SomeClass(2, "Lasha"), new SomeClass(3, "SomeGuy")},
                    false
                },
                new object[]
                {
                    new List<SomeClass>()
                        {new SomeClass(1, "David"), new SomeClass(-2, "Lasha"), new SomeClass(3, "Tamta")},
                    false
                }
            };
        #endregion

        [Test]
        [TestCaseSource(nameof(ParametersWithoutWhere))]
        public void AllWithoutWhere<T>(List<T> data, bool expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<T>(readerMoq);
            var moq = CreateConnectionMock<T>(commandMoq);

            var actual = IDvlSql.DefaultDvlSql(moq.Object)
                .From(TableName)
                .Select()
                .AllAsync()
                .Result;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCaseSource(nameof(ParametersWithWhereFor5Letters))]
        public void AllWithWheref1IsPositive<T>(List<T> data, bool expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<bool>(readerMoq);
            var moq = CreateConnectionMock<bool>(commandMoq);

            var actual = IDvlSql.DefaultDvlSql(moq.Object)
                .From(TableName)
                .Where(ConstantExp("f1", true) > 0)
                .Select()
                .AllAsync()
                .Result;

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
