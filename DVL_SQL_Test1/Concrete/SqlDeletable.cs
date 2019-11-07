﻿using System.Collections.Generic;
using System.Linq;
using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DVL_SQL_Test1.Models;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlDeletable : IDeletable
    {
        private readonly IDvlSqlConnection _dvlSqlConnection;
        private readonly DvlSqlDeleteExpression _deleteExpression;
        private IInsertDeleteExecutable _deleteExecutable;

        public SqlDeletable(DvlSqlFromExpression fromExpression, IDvlSqlConnection dvlSqlConnection)
        {
            (this._deleteExpression, this._dvlSqlConnection) =
                (new DvlSqlDeleteExpression(fromExpression), dvlSqlConnection);
            this._deleteExecutable = new SqlInsertDeleteExecutable(this._dvlSqlConnection,
                GetSqlString, GetDvlSqlParameters);
        }

        public async Task<int> ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default) =>
            await this._deleteExecutable.ExecuteAsync(timeout, cancellationToken);

        private string GetSqlString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this._deleteExpression.Accept(commandBuilder);

            return builder.ToString();
        }

        public IInsertDeleteExecutable Where(DvlSqlBinaryExpression binaryExpression)
        {
            this._deleteExpression.WhereExpression = new DvlSqlWhereExpression(binaryExpression);
            return this._deleteExecutable =
                new SqlInsertDeleteExecutable(this._dvlSqlConnection, GetSqlString, GetDvlSqlParameters);
        }

        public IInsertDeleteExecutable Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params)
        {
            this._deleteExpression.WhereExpression = new DvlSqlWhereExpression(binaryExpression).WithParameters(@params) as DvlSqlWhereExpression;
            return this._deleteExecutable =
                new SqlInsertDeleteExecutable(this._dvlSqlConnection, GetSqlString, GetDvlSqlParameters);
        }

        private IEnumerable<DvlSqlParameter> GetDvlSqlParameters() => this._deleteExpression.WhereExpression?.Parameters ?? Enumerable.Empty<DvlSqlParameter>();
    }
}
