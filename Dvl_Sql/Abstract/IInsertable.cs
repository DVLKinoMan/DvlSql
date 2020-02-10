using System.Runtime.CompilerServices;
using Dvl_Sql.Expressions;
using Dvl_Sql.Models;

namespace Dvl_Sql.Abstract
{
    // ReSharper disable once IdentifierTypo
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IInsertable<TParam> where TParam : ITuple
    {
        IInsertDeleteExecutable Values(params TParam[] @params);
    }

    // public interface IInsertable<T1>
    // {
    //     IInsertDeleteExecutable Values(T1 param1);
    // }

    public interface IInsertable<T1, T2>
    {
        IInsertDeleteExecutable Values(params (T1 param1, T2 param2)[] @params);
    }

    public interface IInsertable<T1, T2, T3>
    {
        IInsertDeleteExecutable Values(params (T1 param1, T2 param2, T3 param3)[] @params);
    }

    public interface IInsertable<T1, T2, T3, T4>
    {
        IInsertDeleteExecutable Values(params (T1 param1, T2 param2, T3 param3, T4 param4)[] @params);
    }

    public interface IInsertable<T1, T2, T3, T4, T5>
    {
        IInsertDeleteExecutable Values(params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)[] @params);
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6>
    {
        IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)[] @params);
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7>
    {
        IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)[] @params);
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8)[] @params);
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9)[]
                @params);
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10)[] @params);
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    {
        IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11)[] @params);
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    {
        IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12)[] @params);
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    {
        IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13)[] @params);
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    {
        IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13, T14 param14)[] @params);
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    {
        IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13, T14 param14, T15 param15)[] @params);
    }

    public interface IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
    {
        IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13, T14 param14, T15 param15, T16 param16)[] @params);
    }

    // ReSharper disable once IdentifierTypo
    public interface IInsertable
    {
        IInsertDeleteExecutable SelectStatement(DvlSqlFullSelectExpression selectExpression,
            params DvlSqlParameter[] @params);
    }
}