using DvlSql.Expressions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using static DvlSql.Extensions.ExpressionHelpers;
using static DvlSql.Extensions.SqlType;
using static System.Extensions.Exts;

namespace DvlSql.SqlServer
{
    internal class BaseInsertable<TParam> where TParam : ITuple
    {
        protected DvlSqlInsertExpression InsertIntoExpression;
        protected readonly IDvlSqlConnection DvlSqlConnection;

        public BaseInsertable(DvlSqlInsertExpression insertIntoExpression, IDvlSqlConnection conn)
        {
            InsertIntoExpression = insertIntoExpression;
            DvlSqlConnection = conn;
        }

        protected void SetOutputExpression(DvlSqlTableDeclarationExpression intoTable, string[] cols)
        {
            this.InsertIntoExpression.OutputExpression = OutputExp(intoTable, cols);
        }

        protected void SetOutputExpression(string[] cols)
        {
            this.InsertIntoExpression.OutputExpression = OutputExp(cols);
        }

        protected IInsertDeleteExecutable<int> Values(params TParam[] @params)
        {
            var insertInto = this.InsertIntoExpression as DvlSqlInsertIntoExpression<TParam> 
                             ?? throw new Exception("Can not Convert to Generic insert Expression");
            insertInto.ValuesExpression = ValuesExp(@params);
            insertInto.WithParameters(GetSqlParameters(@params.ToTuples().ToArray(), insertInto.DvlSqlTypes));
            insertInto.ValuesExpression.SqlParameters = insertInto.Parameters;

            return new SqlInsertDeleteExecutable<int>(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters,
                (command, timeout, token) => command.ExecuteNonQueryAsync(timeout, token ?? default));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this.InsertIntoExpression.Accept(commandBuilder);

            return builder.ToString();
        }

        protected IEnumerable<DvlSqlParameter> GetDvlSqlParameters() => this.InsertIntoExpression?.Parameters;
    }

    internal class BaseOutputable<TParam, TResult> : BaseInsertable<TParam> where TParam : ITuple
    {
        protected readonly Func<IDataReader, TResult> Reader;

        public BaseOutputable(DvlSqlInsertExpression insertIntoExpression, IDvlSqlConnection conn,
            Func<IDataReader, TResult> func) : base(insertIntoExpression, conn) => this.Reader = func;

        protected new IInsertDeleteExecutable<TResult> Values(params TParam[] @params)
        {
            var insertInto = this.InsertIntoExpression as DvlSqlInsertIntoExpression<TParam>
                             ?? throw new Exception("Can not Convert to Generic insert Expression");
            insertInto.ValuesExpression = ValuesExp(@params);
            insertInto.WithParameters(GetSqlParameters(@params.ToTuples().ToArray(), insertInto.DvlSqlTypes));
            insertInto.ValuesExpression.SqlParameters = insertInto.Parameters;

            return new SqlInsertDeleteExecutable<TResult>(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters,
                (command, timeout, token) =>
                    command.ExecuteReaderAsync(Reader, timeout, cancellationToken: token ?? default));
        }

        protected IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect)
        {
            this.InsertIntoExpression = new DvlSqlInsertIntoSelectExpression(this.InsertIntoExpression.TableName,
                this.InsertIntoExpression.Columns) {SelectExpression = fullSelect,
                OutputExpression = InsertIntoExpression.OutputExpression};

            if (fullSelect.From is DvlSqlValuesExpression valuesExp)
                this.InsertIntoExpression.WithParameters(valuesExp.SqlParameters);

            return new SqlInsertDeleteExecutable<TResult>(this.DvlSqlConnection, ToString,
                GetDvlSqlParameters,
                (command, timeout, token) =>
                    command.ExecuteReaderAsync(Reader, timeout, cancellationToken: token ?? default));
        }
    }

    // ReSharper disable once IdentifierTypo
    internal class SqlInsertable<TParam> : BaseInsertable<TParam>, IInsertable<TParam> where TParam : ITuple
    {
        // ReSharper disable once IdentifierTypo
        public SqlInsertable(DvlSqlInsertIntoExpression<TParam> insertIntoExpression, IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertOutputable<TParam, TResult> Output<TResult>(Func<IDataReader, TResult> reader,
            params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<TParam, TResult>(InsertIntoExpression, DvlSqlConnection, reader);
        }

        public new IInsertDeleteExecutable<int> Values(params TParam[] @params) => base.Values(@params);
    }

    internal class InsertOutputable<TParam, TResult> : BaseOutputable<TParam, TResult>,
        IInsertOutputable<TParam, TResult>
        where TParam : ITuple
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(DvlSqlInsertExpression insertIntoExpression, IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult> Values(params TParam[] @params) => base.Values(@params);
    }

    internal class Insertable<T1, T2> : BaseInsertable<(T1, T2)>, IInsertable<T1, T2>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2)> insertIntoExpression, IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertable<T1, T2> Output(DvlSqlTableDeclarationExpression intoTable, params string[] cols)
        {
            this.SetOutputExpression(intoTable, cols);
            return this;
        }

        public IInsertOutputable<T1, T2, TResult> Output<TResult>(Func<IDataReader, TResult> reader,
            params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<T1, T2, TResult>(InsertIntoExpression, DvlSqlConnection, reader);
        }

        IInsertDeleteExecutable<int> IInsertable<T1, T2>.Values(params (T1 param1, T2 param2)[] @params) =>
            base.Values(@params);
    }

    internal class InsertOutputable<T1, T2, TResult> : BaseOutputable<(T1, T2), TResult>,
        IInsertOutputable<T1, T2, TResult>
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(DvlSqlInsertExpression insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult> Values(params (T1, T2)[] @params) => base.Values(@params);
    }

    internal class Insertable<T1, T2, T3> : BaseInsertable<(T1, T2, T3)>, IInsertable<T1, T2, T3>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3)> insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertable<T1, T2, T3> Output(DvlSqlTableDeclarationExpression intoTable, params string[] cols)
        {
            this.SetOutputExpression(intoTable, cols);
            return this;
        }


        public IInsertOutputable<T1, T2, T3, TResult> Output<TResult>(Func<IDataReader, TResult> reader,
            params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<T1, T2, T3, TResult>(InsertIntoExpression, DvlSqlConnection, reader);
        }

        public new IInsertDeleteExecutable<int> Values(params (T1 param1, T2 param2, T3 param3)[] @params) =>
            base.Values(@params);
    }

    internal class InsertOutputable<T1, T2, T3, TResult> : BaseOutputable<(T1, T2, T3), TResult>,
        IInsertOutputable<T1, T2, T3, TResult>
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(DvlSqlInsertExpression insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult> Values(params (T1, T2, T3)[] @params) => base.Values(@params);
    }

    internal class Insertable<T1, T2, T3, T4> : BaseInsertable<(T1, T2, T3, T4)>, IInsertable<T1, T2, T3, T4>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4)> insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertable<T1, T2, T3, T4> Output(DvlSqlTableDeclarationExpression intoTable, params string[] cols)
        {
            this.SetOutputExpression(intoTable, cols);
            return this;
        }


        public IInsertOutputable<T1, T2, T3, T4, TResult> Output<TResult>(Func<IDataReader, TResult> reader,
            params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<T1, T2, T3, T4, TResult>(InsertIntoExpression, DvlSqlConnection, reader);
        }

        public new IInsertDeleteExecutable<int> Values(params (T1 param1, T2 param2, T3 param3, T4 param4)[] @params) =>
            base.Values(@params);
    }

    internal class InsertOutputable<T1, T2, T3, T4, TResult> : BaseOutputable<(T1, T2, T3, T4), TResult>,
        IInsertOutputable<T1, T2, T3, T4, TResult>
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(DvlSqlInsertExpression insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult> Values(params (T1, T2, T3, T4)[] @params) => base.Values(@params);
    }

    internal class Insertable<T1, T2, T3, T4, T5> : BaseInsertable<(T1, T2, T3, T4, T5)>,
        IInsertable<T1, T2, T3, T4, T5>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5)> insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertable<T1, T2, T3, T4, T5> Output(DvlSqlTableDeclarationExpression intoTable, params string[] cols)
        {
            this.SetOutputExpression(intoTable, cols);
            return this;
        }

        public IInsertOutputable<T1, T2, T3, T4, T5, TResult> Output<TResult>(Func<IDataReader, TResult> reader,
            params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<T1, T2, T3, T4, T5, TResult>(InsertIntoExpression, DvlSqlConnection, reader);
        }

        public new IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)[] @params) => base.Values(@params);
    }

    internal class InsertOutputable<T1, T2, T3, T4, T5, TResult> : BaseOutputable<(T1, T2, T3, T4, T5), TResult>,
        IInsertOutputable<T1, T2, T3, T4, T5, TResult>
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(DvlSqlInsertExpression insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult> Values(params (T1, T2, T3, T4, T5)[] @params) =>
            base.Values(@params);
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6> : BaseInsertable<(T1, T2, T3, T4, T5, T6)>,
        IInsertable<T1, T2, T3, T4, T5, T6>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6)> insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertable<T1, T2, T3, T4, T5, T6> Output(DvlSqlTableDeclarationExpression intoTable,
            params string[] cols)
        {
            this.SetOutputExpression(intoTable, cols);
            return this;
        }

        public IInsertOutputable<T1, T2, T3, T4, T5, T6, TResult> Output<TResult>(Func<IDataReader, TResult> reader,
            params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<T1, T2, T3, T4, T5, T6, TResult>(InsertIntoExpression, DvlSqlConnection, reader);
        }

        public new IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)[] @params) =>
            base.Values(@params);
    }

    internal class InsertOutputable<T1, T2, T3, T4, T5, T6, TResult> :
        BaseOutputable<(T1, T2, T3, T4, T5, T6), TResult>, IInsertOutputable<T1, T2, T3, T4, T5, T6, TResult>
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(DvlSqlInsertExpression insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult> Values(params (T1, T2, T3, T4, T5, T6)[] @params) =>
            base.Values(@params);
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7> : BaseInsertable<(T1, T2, T3, T4, T5, T6, T7)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7)> insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7> Output(DvlSqlTableDeclarationExpression intoTable,
            params string[] cols)
        {
            this.SetOutputExpression(intoTable, cols);
            return this;
        }

        public IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, TResult> Output<TResult>(Func<IDataReader, TResult> reader,
            params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<T1, T2, T3, T4, T5, T6, T7, TResult>(InsertIntoExpression, DvlSqlConnection,
                reader);
        }

        public new IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)[] @params) =>
            base.Values(@params);
    }

    internal class InsertOutputable<T1, T2, T3, T4, T5, T6, T7, TResult> :
        BaseOutputable<(T1, T2, T3, T4, T5, T6, T7), TResult>, IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, TResult>
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(DvlSqlInsertExpression insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult> Values(params (T1, T2, T3, T4, T5, T6, T7)[] @params) =>
            base.Values(@params);
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8> : BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8)> insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8> Output(DvlSqlTableDeclarationExpression intoTable,
            params string[] cols)
        {
            this.SetOutputExpression(intoTable, cols);
            return this;
        }

        public IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Output<TResult>(
            Func<IDataReader, TResult> reader, params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(InsertIntoExpression, DvlSqlConnection,
                reader);
        }

        public new IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8)[]
                @params) => base.Values(@params);
    }

    internal class InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, TResult> :
        BaseOutputable<(T1, T2, T3, T4, T5, T6, T7, T8), TResult>,
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, TResult>
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(DvlSqlInsertExpression insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult> Values(params (T1, T2, T3, T4, T5, T6, T7, T8)[] @params) =>
            base.Values(@params);
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>, IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9> Output(DvlSqlTableDeclarationExpression intoTable,
            params string[] cols)
        {
            this.SetOutputExpression(intoTable, cols);
            return this;
        }

        public IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Output<TResult>(
            Func<IDataReader, TResult> reader, params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(InsertIntoExpression, DvlSqlConnection,
                reader);
        }

        public new IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9)[]
                @params) => base.Values(@params);
    }

    internal class InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> :
        BaseOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9), TResult>,
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(DvlSqlInsertExpression insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult> Values(params (T1, T2, T3, T4, T5, T6, T7, T8, T9)[] @params) =>
            base.Values(@params);
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>, IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Output(DvlSqlTableDeclarationExpression intoTable,
            params string[] cols)
        {
            this.SetOutputExpression(intoTable, cols);
            return this;
        }

        public IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Output<TResult>(
            Func<IDataReader, TResult> reader, params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(InsertIntoExpression,
                DvlSqlConnection, reader);
        }

        public new IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10)[] @params) => base.Values(@params);
    }

    internal class InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> :
        BaseOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), TResult>,
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(DvlSqlInsertExpression insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult>
            Values(params (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)[] @params) => base.Values(@params);
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)> insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Output(
            DvlSqlTableDeclarationExpression intoTable, params string[] cols)
        {
            this.SetOutputExpression(intoTable, cols);
            return this;
        }

        public IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> Output<TResult>(
            Func<IDataReader, TResult> reader, params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(InsertIntoExpression,
                DvlSqlConnection, reader);
        }

        public new IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11)[] @params) => base.Values(@params);
    }

    internal class InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> :
        BaseOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11), TResult>,
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(
            DvlSqlInsertExpression insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult> Values(
            params (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)[] @params) => base.Values(@params);
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(
            DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)> insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Output(
            DvlSqlTableDeclarationExpression intoTable, params string[] cols)
        {
            this.SetOutputExpression(intoTable, cols);
            return this;
        }

        public IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> Output<TResult>(
            Func<IDataReader, TResult> reader, params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(InsertIntoExpression,
                DvlSqlConnection, reader);
        }

        public new IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12)[] @params) => base.Values(@params);
    }

    internal class InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> :
        BaseOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12), TResult>,
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(
            DvlSqlInsertExpression insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult> Values(
            params (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)[] @params) => base.Values(@params);
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(
            DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)> insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Output(
            DvlSqlTableDeclarationExpression intoTable, params string[] cols)
        {
            this.SetOutputExpression(intoTable, cols);
            return this;
        }

        public IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> Output<TResult>(
            Func<IDataReader, TResult> reader, params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
                InsertIntoExpression, DvlSqlConnection, reader);
        }

        public new IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13)[] @params) => base.Values(@params);
    }

    internal class InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> :
        BaseOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13), TResult>,
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(
            DvlSqlInsertExpression insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult> Values(
            params (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)[] @params) => base.Values(@params);
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(
            DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)> insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Output(
            DvlSqlTableDeclarationExpression intoTable, params string[] cols)
        {
            this.SetOutputExpression(intoTable, cols);
            return this;
        }

        public IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> Output<TResult>(
            Func<IDataReader, TResult> reader, params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
                InsertIntoExpression, DvlSqlConnection, reader);
        }

        public new IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13, T14 param14)[] @params) => base.Values(@params);
    }

    internal class InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> :
        BaseOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14), TResult>,
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(
            DvlSqlInsertExpression insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult> Values(
            params (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)[] @params) => base.Values(@params);
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(
            DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>
                insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Output(
            DvlSqlTableDeclarationExpression intoTable, params string[] cols)
        {
            this.SetOutputExpression(intoTable, cols);
            return this;
        }

        public IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>
            Output<TResult>(Func<IDataReader, TResult> reader, params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
                InsertIntoExpression, DvlSqlConnection, reader);
        }

        public new IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13, T14 param14, T15 param15)[] @params) =>
            base.Values(@params);
    }

    internal class InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> :
        BaseOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15), TResult>,
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(
            DvlSqlInsertExpression
                insertIntoExpression, IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult> Values(
            params (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)[] @params) =>
            base.Values(@params);
    }

    internal class Insertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> :
        BaseInsertable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>,
        IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
    {
        // ReSharper disable once IdentifierTypo
        public Insertable(
            DvlSqlInsertIntoExpression<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>
                insertIntoExpression,
            IDvlSqlConnection dvlSqlConnection) :
            base(insertIntoExpression, dvlSqlConnection)
        {

        }

        public IInsertable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Output(
            DvlSqlTableDeclarationExpression intoTable, params string[] cols)
        {
            this.SetOutputExpression(intoTable, cols);
            return this;
        }

        public IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>
            Output<TResult>(Func<IDataReader, TResult> reader, params string[] cols)
        {
            this.SetOutputExpression(cols);
            return new InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
                InsertIntoExpression, DvlSqlConnection, reader);
        }

        public new IInsertDeleteExecutable<int> Values(
            params (T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9,
                T10 param10, T11 param11, T12 param12, T13 param13, T14 param14, T15 param15, T16 param16)[] @params) =>
            base.Values(@params);
    }

    internal class InsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> :
        BaseOutputable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16), TResult>,
        IInsertOutputable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>
    {
        // ReSharper disable once IdentifierTypo
        public InsertOutputable(
            DvlSqlInsertExpression
                insertIntoExpression, IDvlSqlConnection dvlSqlConnection,
            Func<IDataReader, TResult> reader) : base(insertIntoExpression, dvlSqlConnection, reader)
        {
        }

        public new IInsertDeleteExecutable<TResult> Select(DvlSqlFullSelectExpression fullSelect) =>
            base.Select(fullSelect);

        public new IInsertDeleteExecutable<TResult> Values(
            params (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)[] @params) =>
            base.Values(@params);
    }

    // ReSharper disable once IdentifierTypo
    internal class SqlInsertable : IInsertable
    {
        private readonly DvlSqlInsertIntoSelectExpression _insertWithSelectExpression;
        private readonly IDvlSqlConnection _dvlSqlConnection;

        // ReSharper disable once IdentifierTypo
        public SqlInsertable(DvlSqlInsertIntoSelectExpression insertExpression, IDvlSqlConnection connectionString) =>
            (this._insertWithSelectExpression, this._dvlSqlConnection) = (insertExpression, connectionString);

        public IInsertDeleteExecutable<int> SelectStatement(DvlSqlFullSelectExpression selectExpression,
            params DvlSqlParameter[] @params)
        {
            this._insertWithSelectExpression.SelectExpression = selectExpression;
            this._insertWithSelectExpression.Parameters = @params.ToList();

            return new SqlInsertDeleteExecutable<int>(this._dvlSqlConnection, ToString, GetDvlSqlParameters,
                (command, timeout, token) => command.ExecuteNonQueryAsync(timeout, token ?? default));
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