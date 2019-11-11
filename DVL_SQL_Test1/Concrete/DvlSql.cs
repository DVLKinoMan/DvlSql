using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using DVL_SQL_Test1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSql : IDvlSql
    {
        private readonly IDvlSqlConnection _dvlSqlConnection;

        public DvlSql(string connectionString) => this._dvlSqlConnection = new DvlSqlConnection(connectionString);

        public ISelector From(string tableName, bool withNoLock = false)
        {
            var fromExpression = new DvlSqlFromExpression(tableName, withNoLock);

            return new SqlSelector(fromExpression, this._dvlSqlConnection);
        }

        public IInsertable<TRes> InsertInto<TRes>(string tableName, params (string col, DvlSqlType sqlType)[] types)
            where TRes : ITuple
        {
            var insertExpression = new DvlSqlInsertIntoExpression<TRes>(tableName, types);

            return new SqlInsertable<TRes>(insertExpression, this._dvlSqlConnection);
        }

        public IInsertable InsertInto(string tableName, IEnumerable<string> cols)
        {
            var insertExpression = new DvlSqlInsertIntoSelectExpression(tableName, cols.ToArray());

            return new SqlInsertable(insertExpression, this._dvlSqlConnection);
        }

        public IDeletable DeleteFrom(string tableName)
        {
            var fromExpression = new DvlSqlFromExpression(tableName);

            return new SqlDeletable(fromExpression, this._dvlSqlConnection);
        }

        public IUpdateSetable Update(string tableName)
        {
            var updateExpression = new DvlSqlUpdateExpression(tableName);

            return new SqlUpdateSetable(this._dvlSqlConnection, updateExpression);
        }

        public IProcedureExecutable Procedure(string procedureName, params DvlSqlParameter[] parameters) =>
            new SqlProcedureExecutable(this._dvlSqlConnection, procedureName, parameters);
    }
}
