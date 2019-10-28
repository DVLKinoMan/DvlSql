using DVL_SQL_Test1.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlExecutor : IExecutor
    {
        private readonly IDvlSqlConnection _connection;
        private readonly DvlSqlSelectable _selectable;
        private string _sqlString;
        private DvlSqlOrderByExpression _sqlOrderByExpression;

        public SqlExecutor(IDvlSqlConnection connection, DvlSqlSelectable selectable) =>
            (this._connection, this._selectable) = (connection, selectable);

        public (int, bool) Execute()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TResult>> ToListAsync<TResult>(Func<SqlDataReader, TResult> selectorFunc)
        {
            return await this._connection.ConnectAsync(dvlCommand => dvlCommand.ExecuteReaderAsync(ConverterFunc),
                this._selectable.func(this._sqlOrderByExpression).ToString());

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
            return await this._connection.ConnectAsync(
                dvlCommand => dvlCommand.ExecuteReaderAsync(ConverterFunc, timeout, behavior, cancellationToken),
                this._selectable.func(this._sqlOrderByExpression).ToString());

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

        public IExecutor OrderBy(params string[] fields)
        {
            if (this._sqlOrderByExpression == null)
                this._sqlOrderByExpression = new DvlSqlOrderByExpression(fields.Select(f => (f, Ascending: Ordering.ASC)));
            else this._sqlOrderByExpression.AddRange(fields.Select(f => (f, Ascending: Ordering.ASC)));

            return this;
        }

        public IExecutor OrderByDescending(params string[] fields)
        {
            if (this._sqlOrderByExpression == null)
                this._sqlOrderByExpression = new DvlSqlOrderByExpression(fields.Select(f => (f, Descending: Ordering.DESC)));
            else this._sqlOrderByExpression.AddRange(fields.Select(f => (f, Descending: Ordering.DESC)));

            return this;
        }

    }
}
