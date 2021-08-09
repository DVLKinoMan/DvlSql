using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using DvlSql.Concrete;
using DvlSql.Expressions;
using DvlSql.Models;

namespace DvlSql.Abstract
{
    public interface IDvlSql : IFromable, IProcedure, ITransaction, IDeclarable
    {
        IInsertDeleteExecutable<int> InsertInto<T>(DvlSqlInsertIntoExpression<T> insert) where T: ITuple;

        IInsertDeleteExecutable<TResult> InsertInto<T, TResult>(DvlSqlInsertIntoExpression<T> insert, 
            Func<IDataReader, TResult> reader,
            params string[] outputCols) 
            where T : ITuple;

        IInsertable<TRes> InsertInto<TRes>(string tableName, params DvlSqlType[] types)
            where TRes : ITuple;

        IInsertable<T1, T2> InsertInto<T1, T2>(string tableName, DvlSqlType type1, DvlSqlType type2);

        IInsertable<T1, T2, T3> InsertInto<T1, T2, T3>(string tableName, DvlSqlType type1, DvlSqlType type2,
            DvlSqlType type3);

        IInsertable<T1, T2, T3, T4> InsertInto<T1, T2, T3, T4>(string tableName, DvlSqlType type1, DvlSqlType type2,
            DvlSqlType type3, DvlSqlType type4);

        IInsertable<T1, T2, T3, T4, T5> InsertInto<T1, T2, T3, T4, T5>(string tableName, DvlSqlType type1,
            DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5);

        IInsertable<T1, T2, T3, T4, T5, T6> InsertInto<T1, T2, T3, T4, T5, T6>(string tableName, DvlSqlType type1,
            DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6);

        IInsertable<T1, T2, T3, T4, T5, T6, T7> InsertInto<T1, T2, T3, T4, T5, T6, T7>(string tableName,
            DvlSqlType type1, DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6,
            DvlSqlType type7);

        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8>(string tableName,
            DvlSqlType type1, DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6,
            DvlSqlType type7, DvlSqlType type8);

        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string tableName,
            DvlSqlType type1, DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6,
            DvlSqlType type7, DvlSqlType type8, DvlSqlType type9);

        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            string tableName, DvlSqlType type1, DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5,
            DvlSqlType type6, DvlSqlType type7, DvlSqlType type8, DvlSqlType type9, DvlSqlType type10);

        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
            InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string tableName, DvlSqlType type1,
                DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6,
                DvlSqlType type7, DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11);

        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
            InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string tableName, DvlSqlType type1,
                DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6,
                DvlSqlType type7, DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11,
                DvlSqlType type12);

        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
            InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string tableName, DvlSqlType type1,
                DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6,
                DvlSqlType type7, DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11,
                DvlSqlType type12, DvlSqlType type13);

        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
            InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string tableName, DvlSqlType type1,
                DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5, DvlSqlType type6,
                DvlSqlType type7, DvlSqlType type8, DvlSqlType type9, DvlSqlType type10, DvlSqlType type11,
                DvlSqlType type12, DvlSqlType type13, DvlSqlType type14);

        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
            InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string tableName,
                DvlSqlType type1, DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5,
                DvlSqlType type6, DvlSqlType type7, DvlSqlType type8, DvlSqlType type9, DvlSqlType type10,
                DvlSqlType type11, DvlSqlType type12, DvlSqlType type13, DvlSqlType type14, DvlSqlType type15);

        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
            InsertInto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string tableName,
                DvlSqlType type1, DvlSqlType type2, DvlSqlType type3, DvlSqlType type4, DvlSqlType type5,
                DvlSqlType type6, DvlSqlType type7, DvlSqlType type8, DvlSqlType type9, DvlSqlType type10,
                DvlSqlType type11, DvlSqlType type12, DvlSqlType type13, DvlSqlType type14, DvlSqlType type15,
                DvlSqlType type16);

        IInsertable InsertInto(string tableName, IEnumerable<string> cols);
        IDeletable DeleteFrom(string tableName);
        IDeletable DeleteFrom(DvlSqlFromWithTableExpression fromExpression);
        IUpdateSetable Update(string tableName);

        IDvlSql SetConnection(IDvlSqlConnection connection);

        public static IDvlSql DefaultDvlSql(string connectionString) => new DvlSqlImpl(connectionString);
        
        public static IDvlSql DefaultDvlSql(IDvlSqlConnection connection) => new DvlSqlImpl(connection);
    }
}