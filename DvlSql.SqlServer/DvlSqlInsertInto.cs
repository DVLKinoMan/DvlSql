using System;
using DvlSql.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using static DvlSql.ExpressionHelpers;

namespace DvlSql.SqlServer
{
    partial class DvlSqlMs
    {
        public IInsertDeleteExecutable<int> InsertInto<T>(DvlSqlInsertIntoExpression<T> insert) where T : ITuple
        {
            var insertable = new SqlInsertable<T>(insert, GetConnection());

            return insertable.Values(insert.ValuesExpression?.Values);
        }

        public IInsertDeleteExecutable<TResult> InsertInto<T, TResult>(DvlSqlInsertIntoExpression<T> insert, 
            Func<IDataReader, TResult> reader,
            params string[] outputCols) where T : ITuple
        {
            insert.OutputExpression = OutputExp(outputCols);
            var insertOutputable = new InsertOutputable<T, TResult>(insert, GetConnection(), reader);
            return insertOutputable.Values(insert.ValuesExpression?.Values);
        }

        public IInsertable<TRes> InsertInto<TRes>(string tableName, params DvlSqlType[] types)
            where TRes : ITuple
        {
            var insertExpression = new DvlSqlInsertIntoExpression<TRes>(tableName, types);

            return new SqlInsertable<TRes>(insertExpression, GetConnection());
        }

        public IInsertable<T1, T2> InsertInto<T1, T2>(string tableName, DvlSqlType type1, DvlSqlType type2)
        {
            var insertExpression = new DvlSqlInsertIntoExpression<(T1, T2)>(tableName, type1, type2);

            return new Insertable<T1, T2>(insertExpression, GetConnection());
        }

        public IInsertable<T1, T2, T3> InsertInto<T1, T2, T3>(string tableName, DvlSqlType type1, DvlSqlType type2,
            DvlSqlType type3)
        {
            var insertExpression = new DvlSqlInsertIntoExpression<(T1, T2, T3)>(tableName, type1, type2, type3);

            return new Insertable<T1, T2, T3>(insertExpression, GetConnection());
        }

        public IInsertable<T1, T2, T3, T4> InsertInto<T1, T2, T3, T4>(string tableName, DvlSqlType type1,
            DvlSqlType type2, DvlSqlType type3,
            DvlSqlType type4)
        {
            var insertExpression =
                new DvlSqlInsertIntoExpression<(T1, T2, T3, T4)>(tableName, type1, type2, type3, type4);

            return new Insertable<T1, T2, T3, T4>(insertExpression, GetConnection());
        }

        public IInsertable<T1, T2, T3, T4, T5> InsertInto<T1, T2, T3, T4, T5>(string tableName, DvlSqlType type1,
            DvlSqlType type2, DvlSqlType type3,
            DvlSqlType type4, DvlSqlType type5)
        {
            var insertExpression =
                new DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5)>(tableName, type1, type2, type3, type4, type5);

            return new Insertable<T1, T2, T3, T4, T5>(insertExpression, GetConnection());
        }

        public IInsertable<T1, T2, T3, T4, T5, T6> InsertInto<T1, T2, T3, T4, T5, T6>(string tableName,
            DvlSqlType type1, DvlSqlType type2, DvlSqlType type3,
            DvlSqlType type4, DvlSqlType type5, DvlSqlType type6)
        {
            var insertExpression =
                new DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6)>(tableName, type1, type2, type3, type4, type5,
                    type6);

            return new Insertable<T1, T2, T3, T4, T5, T6>(insertExpression, GetConnection());
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7> InsertInto<T1, T2, T3, T4, T5, T6, T7>(string tableName,
            DvlSqlType type1, DvlSqlType type2,
            DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7)
        {
            var insertExpression =
                new DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7)>(tableName, type1, type2, type3, type4,
                    type5, type6, type7);

            return new Insertable<T1, T2, T3, T4, T5, T6, T7>(insertExpression, GetConnection());
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8>(string tableName,
            DvlSqlType type1, DvlSqlType type2,
            DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7, DvlSqlType type8)
        {
            var insertExpression = new DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8)>(tableName, type1,
                type2, type3, type4, type5, type6, type7, type8);

            return new Insertable<T1, T2, T3, T4, T5, T6, T7, T8>(insertExpression, GetConnection());
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            string tableName, DvlSqlType type1, DvlSqlType type2,
            DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7, DvlSqlType type8,
            DvlSqlType type9)
        {
            var insertExpression = new DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>(tableName,
                type1, type2, type3, type4, type5, type6, type7, type8, type9);

            return new Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9>(insertExpression, GetConnection());
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            string tableName, DvlSqlType type1, DvlSqlType type2,
            DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7, DvlSqlType type8,
            DvlSqlType type9, DvlSqlType type10)
        {
            var insertExpression = new DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>(tableName,
                type1, type2, type3, type4, type5, type6, type7, type8, type9, type10);

            return new Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(insertExpression, GetConnection());
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9,
            T10, T11>(string tableName, DvlSqlType type1,
            DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7,
            DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11)
        {
            var insertExpression =
                new DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>(tableName, type1, type2,
                    type3, type4, type5, type6, type7, type8, type9, type10, type11);

            return new Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(insertExpression,
                GetConnection());
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8,
            T9, T10, T11, T12>(string tableName, DvlSqlType type1,
            DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7,
            DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11, DvlSqlType type12)
        {
            var insertExpression = new DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>(
                tableName, type1, type2, type3, type4, type5, type6, type7, type8, type9, type10, type11, type12);

            return new Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(insertExpression,
                GetConnection());
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> InsertInto<T1, T2, T3, T4, T5, T6,
            T7, T8, T9, T10, T11, T12, T13>(string tableName, DvlSqlType type1,
            DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7,
            DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11, DvlSqlType type12,
            DvlSqlType type13)
        {
            var insertExpression =
                new DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>(tableName,
                    type1, type2, type3, type4, type5, type6, type7, type8, type9, type10, type11, type12, type13);

            return new Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(insertExpression,
                GetConnection());
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> InsertInto<T1, T2, T3, T4, T5,
            T6, T7, T8, T9, T10, T11, T12, T13, T14>(string tableName, DvlSqlType type1,
            DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6, DvlSqlType type7,
            DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11, DvlSqlType type12,
            DvlSqlType type13,
            DvlSqlType type14)
        {
            var insertExpression =
                new DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>(tableName,
                    type1, type2, type3, type4, type5, type6, type7, type8, type9, type10, type11, type12, type13,
                    type14);

            return new Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(insertExpression,
                GetConnection());
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> InsertInto<T1, T2, T3, T4,
            T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string tableName,
            DvlSqlType type1, DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6,
            DvlSqlType type7, DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11,
            DvlSqlType type12,
            DvlSqlType type13, DvlSqlType type14, DvlSqlType type15)
        {
            var insertExpression =
                new DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>(
                    tableName, type1, type2, type3, type4, type5, type6, type7, type8, type9, type10, type11, type12,
                    type13, type14, type15);

            return new Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(insertExpression,
                GetConnection());
        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> InsertInto<T1, T2, T3,
            T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string tableName,
            DvlSqlType type1, DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6,
            DvlSqlType type7, DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11,
            DvlSqlType type12,
            DvlSqlType type13, DvlSqlType type14, DvlSqlType type15, DvlSqlType type16)
        {
            var insertExpression =
                new DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>(
                    tableName, type1, type2, type3, type4, type5, type6, type7, type8, type9, type10, type11, type12,
                    type13, type14, type15, type16);

            return new Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
                insertExpression, GetConnection());
        }

        public IInsertable InsertInto(string tableName, IEnumerable<string> cols)
        {
            var insertExpression = new DvlSqlInsertIntoSelectExpression(tableName, cols.ToArray());

            return new SqlInsertable(insertExpression, GetConnection());
        }
    }
}
