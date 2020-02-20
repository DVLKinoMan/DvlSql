using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dvl_Sql.Abstract;
using Moq;

namespace Dvl_Sql.Tests.Result
{
    public static class Helpers
    {
        public static Mock<IDataReader> CreateDataReaderMock<T>(IList<T> list)
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

        public static Mock<IDvlSqlCommand> CreateSqlCommandMock<T>(Mock<IDataReader> readerMoq)
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

        public static Mock<IDvlSqlConnection> CreateConnectionMock<T>(Mock<IDvlSqlCommand> commandMoq)
        {
            var moq = new Mock<IDvlSqlConnection>();

            moq.Setup(m => m.ConnectAsync(It.IsAny<Func<IDvlSqlCommand, Task<T>>>(),
                    It.IsAny<string>(), It.Is<CommandType>(com => com == CommandType.Text),
                    It.IsAny<SqlParameter[]>()))
                .Returns((Func<IDvlSqlCommand, Task<T>> func, string sqlString,
                    CommandType commandType, SqlParameter[] parameters) => func(commandMoq.Object));

            return moq;
        }
    }
}