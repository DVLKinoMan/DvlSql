using DVL_SQL_Test1.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlExecutor : IExecuter
    {
        //private readonly Func<SqlDataReaderType, CancellationToken, Task<SqlDataReader>> _sqlReaderAsync;
        //private CancellationToken _cancellationToken;
        private DvlSqlCommand _command;

        public SqlExecutor(DvlSqlCommand command) =>
            this._command = command;

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

            var reader = await this._command.ReadAsync(SqlDataReaderType.ExecuteReaderAsync, cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
                list.Add((TResult) reader[0]);

            return list;
        }

        public TResult FirstOrDefault<TResult>()
        {
            throw new NotImplementedException();
        }

    }
}
