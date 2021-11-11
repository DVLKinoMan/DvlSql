using DvlSql.SqlServer;
using DvlSql.Tests.Classes;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static DvlSql.ExpressionHelpers;
using static DvlSql.Tests.Result.Helpers;

namespace DvlSql.Tests.Result
{
    [TestFixture]
    public class Any
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
                    new List<string>(), false
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
                    false
                }
            };

        private static readonly object[] ParametersWithWhereFor5Letters =
            new[]
            {
                new object[]
                {
                    new List<SomeClass>()
                        {new SomeClass(1, "David"), new SomeClass(2, "Lasha"), new SomeClass(3, "SomeGuy")},
                    true
                },
                new object[]
                {
                    new List<SomeClass>()
                        {new SomeClass(-1, "David"), new SomeClass(-2, "Lasha"), new SomeClass(-3, "SomeGuy")},
                    true
                }
            };
        #endregion


        public static Mock<IDvlSqlConnection> CreateConnectionMockWithSqlStringContains<T>(Mock<IDvlSqlCommand> commandMoq, CommandType commandType = CommandType.Text)
        {
            var moq = new Mock<IDvlSqlConnection>();

            moq.Setup(m => m.ConnectAsync(It.IsAny<Func<IDvlSqlCommand, Task<T>>>(),
                    It.Is<string>(s => s.Contains("SELECT TOP 1")), It.Is<CommandType>(com => com == commandType),
                    It.IsAny<DvlSqlParameter[]>()))
                .Returns((Func<IDvlSqlCommand, Task<T>> func, string sqlString,
                    CommandType type, DvlSqlParameter[] parameters) => func(commandMoq.Object));

            return moq;
        }

        [Test]
        [TestCaseSource(nameof(ParametersWithoutWhere))]
        public void AnyWithoutWhere<T>(List<T> data, bool expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<bool>(readerMoq);
            var moq = CreateConnectionMockWithSqlStringContains<bool>(commandMoq);

            var actual = new DvlSqlMs(moq.Object)
                .From(TableName)
                .Select()
                .AnyAsync()
                .Result;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCaseSource(nameof(ParametersWithWhereFor5Letters))]
        public void AnyWithWheref1isPositive<T>(List<T> data, bool expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<bool>(readerMoq);
            var moq = CreateConnectionMockWithSqlStringContains<bool>(commandMoq);

            var actual = new DvlSqlMs(moq.Object)
                .From(TableName)
                .Where(ConstantExp("f1", true) > 0)
                .Select()
                .AnyAsync()
                .Result;

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
