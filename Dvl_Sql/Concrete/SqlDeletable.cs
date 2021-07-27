using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DvlSql.Abstract;
using DvlSql.Expressions;
using DvlSql.Models;
using static DvlSql.ExpressionHelpers;

namespace DvlSql.Concrete
{
    internal class SqlDeletable : RemoveOutputable<int>, IDeletable
    {
        public SqlDeletable(DvlSqlFromExpression fromExpression, IDvlSqlConnection dvlSqlConnection) :
            base(new DvlSqlDeleteExpression(fromExpression), dvlSqlConnection,
                (command, timeout, token) => command.ExecuteNonQueryAsync(timeout, token ?? default))
        {

        }

        protected void SetOutputExpression(DvlSqlTableDeclarationExpression intoTable, string[] cols)
        {
            this.DeleteExpression.OutputExpression = OutputExp(intoTable, cols);
        }

        protected void SetOutputExpression(string[] cols)
        {
            this.DeleteExpression.OutputExpression = OutputExp(cols);
        }

        public IDeleteOutputable<TResult> Output<TResult>(Func<IDataReader, TResult> reader, params string[] cols)
        {
            SetOutputExpression(cols);
            return new RemoveOutputable<TResult>(this.DeleteExpression, DvlSqlConnection,
                (command, timeout, token) => command.ExecuteReaderAsync(reader, timeout, CommandBehavior.Default, 
                    token ?? default));
        }
        
        public async Task<int> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default) =>
            await GetInsertDeleteExecutable().ExecuteAsync(timeout, cancellationToken);
    }

    internal class RemoveOutputable<TResult> : IDeleteOutputable<TResult>
    {
        protected readonly DvlSqlDeleteExpression DeleteExpression;
        protected readonly IDvlSqlConnection DvlSqlConnection;
        private readonly Func<IDvlSqlCommand, int?, CancellationToken?, Task<TResult>> _executeQuery;

        public RemoveOutputable(DvlSqlDeleteExpression deleteExpression, IDvlSqlConnection dvlSqlConnection,
            Func<IDvlSqlCommand, int?, CancellationToken?, 
                Task<TResult>> executeQuery)
        {
            DeleteExpression = deleteExpression;
            DvlSqlConnection = dvlSqlConnection;
            _executeQuery = executeQuery;
        }

        protected IInsertDeleteExecutable<TResult> GetInsertDeleteExecutable() =>
            new SqlInsertDeleteExecutable<TResult>(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters,
                _executeQuery);

        public IInsertDeleteExecutable<TResult> Where(DvlSqlBinaryExpression binaryExpression)
        {
            this.DeleteExpression.WhereExpression = new DvlSqlWhereExpression(binaryExpression);
            return GetInsertDeleteExecutable();
        }

        public IInsertDeleteExecutable<TResult> Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params)
        {
            this.DeleteExpression.WhereExpression = new DvlSqlWhereExpression(binaryExpression).WithParameters(@params)
                as DvlSqlWhereExpression;
            return GetInsertDeleteExecutable();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this.DeleteExpression.Accept(commandBuilder);

            return builder.ToString().RemoveUnnecessaryNewlines();
        }

        private IEnumerable<DvlSqlParameter> GetDvlSqlParameters() => this.DeleteExpression.WhereExpression?.Parameters
                                                                      ?? Enumerable.Empty<DvlSqlParameter>();
    }
}
