using DVL_SQL_Test1.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlExecutor : IExecutor
    {
        private readonly IDvlSqlConnection _connection;

        public SqlExecutor(IDvlSqlConnection connection) =>
            this._connection = connection;

        public (int, bool) Execute()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TResult>> ToListAsync<TResult>(Func<SqlDataReader, TResult> selectorFunc)
        {
            return await this._connection.ConnectAsync(dvlCommand => dvlCommand.ExecuteReaderAsync(ConverterFunc));

            List<TResult> ConverterFunc(SqlDataReader reader)
            {
                var list = new List<TResult>();
                while (reader.Read())
                    list.Add(selectorFunc(reader));

                return list;
            }
        }

        public async Task<List<TResult>> ToListAsync<TResult>(int? timeout = default,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default)
        {
            return await this._connection.ConnectAsync(dvlCommand => dvlCommand.ExecuteReaderAsync(ConverterFunc, timeout, behavior, cancellationToken));

            static List<TResult> ConverterFunc(SqlDataReader reader)
            {
                var list = new List<TResult>();
                while (reader.Read())
                    list.Add((TResult) reader[0]);

                return list;
            }
        }

        public TResult FirstOrDefault<TResult>()
        {
            throw new NotImplementedException();
        }

    }
}
