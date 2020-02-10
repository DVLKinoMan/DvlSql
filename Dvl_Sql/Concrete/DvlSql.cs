using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Dvl_Sql.Abstract;
using Dvl_Sql.Expressions;
using Dvl_Sql.Models;

namespace Dvl_Sql.Concrete
{
    internal class DvlSql : IDvlSql
    {
        private readonly IDvlSqlConnection _dvlSqlConnection;

        public DvlSql(string connectionString) => this._dvlSqlConnection = new DvlSqlConnection(connectionString);

        public ISelector From(string tableName, bool withNoLock = false)
        {
            var fromExpression = new DvlSqlFromExpression(tableName, withNoLock);

            return new SqlSelector(fromExpression, this._dvlSqlConnection);
        }

        public IInsertable<TRes> InsertInto<TRes>(string tableName, params DvlSqlType[] types)
            where TRes : ITuple
        {
            var insertExpression = new DvlSqlInsertIntoExpression<TRes>(tableName, types);

            return new SqlInsertable<TRes>(insertExpression, this._dvlSqlConnection);
        }

        public IInsertable<T1, T2> InsertInto<T1, T2>(string tableName, DvlSqlType type1, DvlSqlType type2)
        {
            var insertExpression = new DvlSqlInsertIntoExpression<(T1,T2)>(tableName, type1, type2);

            return new Insertable<T1,T2>(insertExpression, this._dvlSqlConnection);
        }

        public IInsertable<T1, T2, T3> InsertInto<T1, T2, T3>(string tableName, DvlSqlType type1, DvlSqlType type2, DvlSqlType type3)
        {
            throw new System.NotImplementedException();
        }

        public IInsertable<T1, T2, T3, T4> InsertInto<T1, T2, T3, T4>(string tableName, DvlSqlType type1, DvlSqlType type2, DvlSqlType type3,
            DvlSqlType type4)
        {
            throw new System.NotImplementedException();
        }

        public IInsertable<T1, T2, T3, T4, T5> InsertInto<T1, T2, T3, T4, T5>(string tableName, DvlSqlType type1, DvlSqlType type2, DvlSqlType type3,
            DvlSqlType type4, DvlSqlType type5)
        {
            throw new System.NotImplementedException();
        }

        public IInsertable<T1, T2, T3, T4, T5, T6> InsertInto<T1, T2, T3, T4, T5, T6>(string tableName, DvlSqlType type1, DvlSqlType type2, DvlSqlType type3,
            DvlSqlType type4, DvlSqlType type5, DvlSqlType type6)
        {
            throw new System.NotImplementedException();
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7> InsertInto<T1, T2, T3, T4, T5, T6, T7>(string tableName, DvlSqlType type1, DvlSqlType type2,
            DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7)
        {
            throw new System.NotImplementedException();
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8>(string tableName, DvlSqlType type1, DvlSqlType type2,
            DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7, DvlSqlType type8)
        {
            throw new System.NotImplementedException();
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string tableName, DvlSqlType type1, DvlSqlType type2,
            DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7, DvlSqlType type8,
            DvlSqlType type9)
        {
            throw new System.NotImplementedException();
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string tableName, DvlSqlType type1, DvlSqlType type2,
            DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7, DvlSqlType type8,
            DvlSqlType type9, DvlSqlType type10)
        {
            throw new System.NotImplementedException();
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string tableName, DvlSqlType type1,
            DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7,
            DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11)
        {
            throw new System.NotImplementedException();
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string tableName, DvlSqlType type1,
            DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7,
            DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11, DvlSqlType type12)
        {
            throw new System.NotImplementedException();
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string tableName, DvlSqlType type1,
            DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7,
            DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11, DvlSqlType type12, DvlSqlType type13)
        {
            throw new System.NotImplementedException();
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string tableName, DvlSqlType type1,
            DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7,
            DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11, DvlSqlType type12, DvlSqlType type13,
            DvlSqlType type14)
        {
            throw new System.NotImplementedException();
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string tableName,
            DvlSqlType type1, DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6,
            DvlSqlType type7, DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11, DvlSqlType type12,
            DvlSqlType type13, DvlSqlType type14, DvlSqlType type15)
        {
            throw new System.NotImplementedException();
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string tableName,
            DvlSqlType type1, DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6,
            DvlSqlType type7, DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11, DvlSqlType type12,
            DvlSqlType type13, DvlSqlType type14, DvlSqlType type15, DvlSqlType type16)
        {
            throw new System.NotImplementedException();
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

            return new SqlUpdateable(this._dvlSqlConnection, updateExpression);
        }

        public IProcedureExecutable Procedure(string procedureName, params DvlSqlParameter[] parameters) =>
            new SqlProcedureExecutable(this._dvlSqlConnection, procedureName, parameters);
    }
}
