using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dvl_Sql.Abstract;
using Moq;
using NUnit.Framework;

namespace Dvl_Sql.Tests.Result
{
    [TestFixture]
    public class ToList
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(
                "test");

        private string TableName = "dbo.Words";

        private Mock<IDataReader> CreateDataReaderMock<T>(List<T> list)
        {
            var readerMoq = new Mock<IDataReader>();
            int index = 0;

            readerMoq.Setup(reader => reader.Read())
                .Returns(() => index < list.Count);
            // .Callback(() => { index++; });

            readerMoq.Setup(reader => reader[0])
                .Returns(() => list[index++]);


            return readerMoq;
        }

        private Mock<IDvlSqlCommand> CreateSqlCommandMock<T>(Mock<IDataReader> readerMoq)
        {
            var commandMoq = new Mock<IDvlSqlCommand>();

            commandMoq.Setup(com => com.ExecuteReaderAsync(It.IsAny<Func<IDataReader, T>>(),
                    It.IsAny<int?>(),
                    It.IsAny<CommandBehavior>(), It.IsAny<CancellationToken>()))
                .Returns((Func<IDataReader, T> func2, int? timeout,
                        CommandBehavior behavior, CancellationToken cancellationToken) =>
                    Task.FromResult(func2(readerMoq.Object)));

            return commandMoq;
        }

        private Mock<IDvlSqlConnection> CreateConnection<T>(Mock<IDvlSqlCommand> commandMoq)
        {
            var moq = new Mock<IDvlSqlConnection>();

            moq.Setup(m => m.ConnectAsync(It.IsAny<Func<IDvlSqlCommand, Task<T>>>(),
                    It.IsAny<string>(), It.Is<CommandType>(com => com == CommandType.Text),
                    It.IsAny<SqlParameter[]>()))
                .Returns((Func<IDvlSqlCommand, Task<T>> func, string sqlString,
                    CommandType commandType, SqlParameter[] parameters) => func(commandMoq.Object));

            return moq;
        }

        [Test]
        public void TestMethod1()
        {
            var list2 = new List<int>() {1, 2, 3};

            var readerMoq = CreateDataReaderMock(list2);
            var commandMoq = CreateSqlCommandMock<List<int>>(readerMoq);
            var moq = CreateConnection<List<int>>(commandMoq);

            var list = IDvlSql.DefaultDvlSql(moq.Object)
                .From(TableName)
                .Select()
                .ToListAsync(r => (int) r[0] + 1)
                .Result;

            Assert.That(list, Is.EquivalentTo(new List<int>() {2, 3, 4}));
        }

        [Test]
        public void TestMethod2()
        {
            var list2 = new List<string>() {"David", "Lasha", "SomeGuy"};

            var readerMoq = CreateDataReaderMock(list2);
            var commandMoq = CreateSqlCommandMock<List<string>>(readerMoq);
            var moq = CreateConnection<List<string>>(commandMoq);

            var list = IDvlSql.DefaultDvlSql(moq.Object)
                .From(TableName)
                .Select()
                .ToListAsync(r => ((string) r[0]).Substring(0, 1))
                .Result;

            Assert.That(list, Is.EquivalentTo(new List<string>() {"D", "L", "S"}));
        }
    }
}