using System;
using System.Data;
using System.Runtime.CompilerServices;
using DvlSql.Expressions;
using DvlSql.Models;

namespace DvlSql.Abstract
{
    // ReSharper disable once IdentifierTypo
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IInsertable<TParam> where TParam : ITuple
    {
        IInsertOutputable<TParam, TResult> Output<TResult>(Func<IDataReader, TResult> reader, params string[] cols);

        //IInsertable<TParam> Output(DvlSqlTableDeclarationExpression intoTable, params string[] cols);
        IInsertDeleteExecutable<int> Values(params TParam[] @params);
    }

    public interface IInsertOutputable<TParam, TResult> where TParam : ITuple
    {
        IInsertDeleteExecutable<TResult> Values(params TParam[] @params);
    }

    // public interface IInsertable<T1>
    // {
    //     IInsertDeleteExecutable<int>Values(T1 param1);
    // }

    public interface IInsertable<T1, T2>
    {
        IInsertOutputable<T1, T2, TResult> Output<TResult>(Func<IDataReader, TResult> reader, params string[] cols);

        //IInsertable<T1, T2> Output(DvlSqlTableDeclarationExpression intoTable, params string[] cols);
        IInsertDeleteExecutable<int> Values(params (T1 param1, T2 param2)[] @params);
    }

    public interface IInsertOutputable<T1, T2, TResult> : IInsertOutputable<(T1,T2), TResult>
    {
    }

    public interface IInsertable<T1, T2, T3>
    {
        IInsertOutputable<T1, T2, T3, TResult> Output<TResult>(Func<IDataReader, TResult> reader, params string[] cols);

        //IInsertable<T1, T2> Output(DvlSqlTableDeclarationExpression intoTable, params string[] cols);
        IInsertDeleteExecutable<int> Values(params (T1 param1, T2 param2, T3 param3)[] @params);
    }

    public interface IInsertOutputable<T1, T2, T3, TResult> : IInsertOutputable<(T1, T2, T3), TResult>
    {
    }

    public interface IInsertable<T1, T2, T3, T4>
    {
        IInsertOutputable<T1, T2, T3, T4, TResult> Output<TResult>(Func<IDataReader, TResult> reader,
            params string[] cols);

        //IInsertable<T1, T2, T3, T4> Output(DvlSqlTableDeclarationExpression intoTable, params string[] cols);
        IInsertDeleteExecutable<int> Values(params (T1 param1, T2 param2, T3 param3, T4 param4)[] @params);
    }

    public interface IInsertOutputable<T1, T2, T3, T4, TResult> : IInsertOutputable<(T1, T2, T3, T4), TResult>
    {
    }

    public interface IInsertable<T1, T2, T3, T4, T5>
    {
        IInsertOutputable<T1, T2, T3, T4, T5, TResult> Output<TResult>(Func<IDataReader, TResult> reader,
            params string[] cols);

        //IInsertable<T1, T2, T3, T4, T5> Output(DvlSqlTableDeclarationExpression intoTable, params string[] cols);
        IInsertDeleteExecutable<int> Values(params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)[] @params);
    }

    public interface IInsertOutputable<T1, T2, T3, T4, T5, TResult> : IInsertOutputable<(T1, T2, T3, T4, T5), TResult>
    {
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6>
    {
        IInsertOutputable<T1, T2, T3, T4, T5, T6, TResult> Output<TResult>(Func<IDataReader, TResult> reader,
            params string[] cols);
        //IInsertable<T1, T2, T3, T4, T5, T6> Output(DvlSqlTableDeclarationExpression intoTable, params string[] cols);

        IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)[] @params);
    }

    public interface IInsertOutputable<T1, T2, T3, T4, T5, T6, TResult> : IInsertOutputable<(T1, T2, T3, T4, T5, T6), TResult>
    {
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7>
    {
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, TResult>
            Output<TResult>(Func<IDataReader, TResult> reader, params string[] cols);
        //IInsertable<T1, T2, T3, T4, T5, T6, T7>
        //    Output(DvlSqlTableDeclarationExpression intoTable, params string[] cols);

        IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)[] @params);
    }

    public interface IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, TResult> : IInsertOutputable<(T1, T2, T3, T4, T5, T6, T7), TResult>
    {
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Output<TResult>(
            Func<IDataReader, TResult> reader,
            params string[] cols);
        //IInsertable<T1, T2, T3, T4, T5, T6, T7, T8> Output(DvlSqlTableDeclarationExpression intoTable,
        //    params string[] cols);

        IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8)[] @params);
    }

    public interface IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, TResult> 
        : IInsertOutputable<(T1, T2, T3, T4, T5, T6, T7, T8), TResult>
    {
    }


    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Output<TResult>(
            Func<IDataReader, TResult> reader,
            params string[] cols);
        //IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9> Output(DvlSqlTableDeclarationExpression intoTable,
        //    params string[] cols);

        IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9)[]
                @params);
    }

    public interface IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>
        : IInsertOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9), TResult>
    {
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Output<TResult>(
            Func<IDataReader, TResult> reader,
            params string[] cols);
        //IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Output(DvlSqlTableDeclarationExpression intoTable,
        //    params string[] cols);

        IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10)[] @params);
    }

    public interface IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>
        : IInsertOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), TResult>
    {
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    {
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> Output<TResult>(
            Func<IDataReader, TResult> reader,
            params string[] cols);

        //IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Output(DvlSqlTableDeclarationExpression intoTable,
        //    params string[] cols);

        IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11)[] @params);
    }

    public interface IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>
        : IInsertOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11), TResult>
    {
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    {
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> Output<TResult>(
            Func<IDataReader, TResult> reader, params string[] cols);
        //IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Output(
        //    DvlSqlTableDeclarationExpression intoTable, params string[] cols);

        IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12)[] @params);
    }

    public interface IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>
        : IInsertOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12), TResult>
    {
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    {
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> Output<TResult>(
            Func<IDataReader, TResult> reader, params string[] cols);
        //IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Output(
        //    DvlSqlTableDeclarationExpression intoTable, params string[] cols);

        IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13)[] @params);
    }

    public interface IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>
        : IInsertOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13), TResult>
    {
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    {
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> Output<TResult>(
            Func<IDataReader, TResult> reader, params string[] cols);
        //IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Output(
        //    DvlSqlTableDeclarationExpression intoTable, params string[] cols);

        IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13, T14 param14)[] @params);
    }

    public interface IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>
        : IInsertOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14), TResult>
    {
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    {
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> Output<TResult>(
            Func<IDataReader, TResult> reader, params string[] cols);
        //IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Output(
        //    DvlSqlTableDeclarationExpression intoTable, params string[] cols);

        IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13, T14 param14, T15 param15)[] @params);
    }

    public interface IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>
        : IInsertOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15), TResult>
    {
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
    {
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>
            Output<TResult>(Func<IDataReader, TResult> reader, params string[] cols);
        //IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Output(
        //    DvlSqlTableDeclarationExpression intoTable, params string[] cols);

        IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13, T14 param14, T15 param15, T16 param16)[] @params);
    }

    public interface IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>
        : IInsertOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16), TResult>
    {
    }

    // ReSharper disable once IdentifierTypo
    public interface IInsertable
    {
        IInsertDeleteExecutable<int> SelectStatement(DvlSqlFullSelectExpression selectExpression,
            params DvlSqlParameter[] @params);
    }
}