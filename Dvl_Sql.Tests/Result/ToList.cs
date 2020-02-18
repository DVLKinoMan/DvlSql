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

        [Test]
        public void TestMethod1()
        {
            var moq = new Mock<IDvlSqlConnection>();
            // .Returns(Task.FromResult(new List<int>(){1,1,1}));

            moq.Setup(m => m.ConnectAsync(It.IsAny<Func<IDvlSqlCommand, Task<List<int>>>>(),
                    It.IsAny<string>(), It.Is<CommandType>(com => com == CommandType.Text),
                    It.IsAny<SqlParameter[]>()))
                .Returns((Func<IDvlSqlCommand, Task<List<int>>> func, string sqlString,
                    CommandType commandType, SqlParameter[] parameters) =>
                {
                    var commandMoq = new Mock<IDvlSqlCommand>();
                    var readerMoq = new Mock<IDataReader>();
                    var list2 = new List<int>() {1, 2, 3};
                    int index = 0;

                    readerMoq.Setup(reader => reader.Read())
                        .Returns(() => index < list2.Count);
                    // .Callback(() => { index++; });

                    readerMoq.Setup(reader => reader[0])
                        .Returns(() => list2[index++]);

                    commandMoq.Setup(com => com.ExecuteReaderAsync(It.IsAny<Func<IDataReader, List<int>>>(),
                            It.IsAny<int?>(),
                            It.IsAny<CommandBehavior>(), It.IsAny<CancellationToken>()))
                        .Returns((Func<IDataReader, List<int>> func2, int? timeout,
                                CommandBehavior behavior, CancellationToken cancellationToken) =>
                            Task.FromResult(func2(readerMoq.Object)));

                    return func(commandMoq.Object);

                    int Get() => list2[index];
                });
            // .Returns(() => Task.FromResult(new List<int>() {1, 2, 3}));

            // Assert.That(moq.Object.ConnectAsync(f => f.ExecuteReaderAsync(AsList(r => (int) r[0])),
            //     "addd").Result, Is.EquivalentTo(new List<int>() {1, 2, 3}));

            var list = IDvlSql.DefaultDvlSql(moq.Object)
                .From(TableName)
                .Select()
                .ToListAsync(r => (int) r[0] + 1)
                .Result;

            Assert.That(list, Is.EquivalentTo(new List<int>() {2, 3, 4}));
        }
    }
}