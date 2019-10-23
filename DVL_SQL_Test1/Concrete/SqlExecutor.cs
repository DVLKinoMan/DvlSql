using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlExecutor : IExecuter
    {
        private readonly SqlCommand _command;
        //private CancellationToken _cancellationToken;

        public SqlExecutor(SqlCommand command) => this._command = command; //, CancellationToken cancellationToken) =>
            //(this._command, this._cancellationToken) = (command, cancellationToken);

        public (int, bool) Execute()
        {
            throw new NotImplementedException();
        }

        public List<TResult> ToList<TResult>(Func<SqlDataReader, TResult> reader)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TResult>> ToListAsync<TResult>(CancellationToken cancellationToken = default)
        {
            var list = new List<TResult>();
            //using (var connection = new SqlConnection("asdfasdf"))
            //{
            //    await connection.OpenAsync();
            //    var command = new SqlCommand("asdfasd", connection);

                var reader = await this._command.ExecuteReaderAsync(cancellationToken);
                while (await reader.ReadAsync(cancellationToken))
                    list.Add((TResult) reader[0]);
            //}

            return list;
        }

        public TResult FirstOrDefault<TResult>()
        {
            throw new NotImplementedException();
        }

    }
}
