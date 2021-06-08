using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Dvl_Sql.Abstract;
using Dvl_Sql.Expressions;
using Dvl_Sql.Helpers;
using Dvl_Sql.Models;

namespace Dvl_Sql.Concrete
{
    internal class BaseInsertable<TParam> where TParam : ITuple
    {
        protected DvlSqlInsertIntoExpression<TParam> InsertExpression;
        protected IDvlSqlConnection DvlSqlConnection;

        protected static IEnumerable<DvlSqlParameter> GetSqlParameters<TTuple>(TTuple[] @params, DvlSqlType[] types)
            where TTuple : ITuple
        {
            int count = 1;
            foreach (var param in @params)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    var type = typeof(DvlSqlType<>).MakeGenericType(param[i].GetType());
                    var dvlSqlType =
                        Activator.CreateInstance(type,
                            new[] {param[i], types[i], false});//added false value, maybe not right
                    var type2 = typeof(DvlSqlParameter<>).MakeGenericType(param[i].GetType());
                    string name = $"{types[i].Name.WithAlpha()}{count}";
                    yield return (DvlSqlParameter) Activator.CreateInstance(type2, new object[] {name, dvlSqlType});
                }

                count++;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this.InsertExpression.Accept(commandBuilder);

            return builder.ToString();
        }

        protected IEnumerable<DvlSqlParameter> GetDvlSqlParameters() => this.InsertExpression.SqlParameters;
    }

    // ReSharper disable once IdentifierTypo
    internal class SqlInsertable<TParam> : BaseInsertable<TParam>, IInsertable<TParam> where TParam : ITuple
    {
        // ReSharper disable once IdentifierTypo
        public SqlInsertable(DvlSqlInsertIntoExpression<TParam> insertExpression, IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(params TParam[] @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }

    internal class Insertable<T1, T2> : BaseInsertable<(T1, T2)>, IInsertable<T1, T2>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2)> insertExpression, IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(params (T1 param1, T2 param2)[] @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }

    internal class Insertable<T1, T2, T3> : BaseInsertable<(T1, T2, T3)>, IInsertable<T1, T2, T3>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3)> insertExpression,
            IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(params (T1 param1, T2 param2, T3 param3)[] @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }

    internal class Insertable<T1, T2, T3, T4> : BaseInsertable<(T1, T2, T3, T4)>, IInsertable<T1, T2, T3, T4>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4)> insertExpression,
            IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(params (T1 param1, T2 param2, T3 param3, T4 param4)[] @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }


    internal class Insertable<T1, T2, T3, T4, T5> : BaseInsertable<(T1, T2, T3, T4, T5)>,
        IInsertable<T1, T2, T3, T4, T5>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5)> insertExpression,
            IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)[] @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6> : BaseInsertable<(T1, T2, T3, T4, T5, T6)>,
        IInsertable<T1, T2, T3, T4, T5, T6>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6)> insertExpression,
            IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)[] @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7> : BaseInsertable<(T1, T2, T3, T4, T5, T6, T7)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7)> insertExpression,
            IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)[] @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8> : BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8)> insertExpression,
            IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8)[] @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>, IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> insertExpression,
            IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9)[]
                @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>, IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> insertExpression,
            IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10)[] @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)> insertExpression,
            IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11)[] @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(
            DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)> insertExpression,
            IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12)[] @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(
            DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)> insertExpression,
            IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13)[] @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(
            DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)> insertExpression,
            IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13, T14 param14)[] @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(
            DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)> insertExpression,
            IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13, T14 param14, T15 param15)[] @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }
    
    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(
            DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)> insertExpression,
            IDvlSqlConnection dvlSqlConnection) =>
            (this.InsertExpression, this.DvlSqlConnection) = (insertExpression, dvlSqlConnection);

        public IInsertDeleteExecutable Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13, T14 param14, T15 param15, T16 param16)[] @params)
        {
            this.InsertExpression.Values = @params;

            this.InsertExpression.SqlParameters = GetSqlParameters(@params, this.InsertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters);
        }
    }
    
    // ReSharper disable once IdentifierTypo
    internal class SqlInsertable : IInsertable
    {
        private readonly DvlSqlInsertIntoSelectExpression _insertWithSelectExpression;
        private readonly IDvlSqlConnection _dvlSqlConnection;

        // ReSharper disable once IdentifierTypo
        public SqlInsertable(DvlSqlInsertIntoSelectExpression insertExpression, IDvlSqlConnection connectionString) =>
            (this._insertWithSelectExpression, this._dvlSqlConnection) = (insertExpression, connectionString);

        public IInsertDeleteExecutable SelectStatement(DvlSqlFullSelectExpression selectExpression,
            params DvlSqlParameter[] @params)
        {
            this._insertWithSelectExpression.SelectExpression = selectExpression;
            this._insertWithSelectExpression.Parameters = @params.ToList();

            return new SqlInsertDeleteExecutable(this._dvlSqlConnection, ToString, GetDvlSqlParameters);
        }

        private IEnumerable<DvlSqlParameter> GetDvlSqlParameters() => this._insertWithSelectExpression.Parameters;

        public override string ToString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this._insertWithSelectExpression.Accept(commandBuilder);

            return builder.ToString();
        }
    }
}