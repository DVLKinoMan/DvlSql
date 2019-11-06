using System;
using System.Collections.Generic;
using System.Linq;
using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using DVL_SQL_Test1.Models;

namespace DVL_SQL_Test1.Concrete
{
    // ReSharper disable once IdentifierTypo
    public class SqlInsertable<TParam> : IInsertable<TParam> where TParam : ITuple
    {
        private readonly DvlSqlInsertIntoExpression<TParam> _insertExpression;
        private readonly string _connectionString;

        // ReSharper disable once IdentifierTypo
        public SqlInsertable(DvlSqlInsertIntoExpression<TParam> insertExpression, string connectionString) =>
            (this._insertExpression, this._connectionString) = (insertExpression, connectionString);

        public IInsertDeleteExecutable Values(params TParam[] @params)
        {
            this._insertExpression.Values = @params;

            this._insertExpression.SqlParameters = GetSqlParameters(@params, this._insertExpression.DvlSqlTypes).ToList();

            return new SqlInsertDeleteExecutable(new DvlSqlConnection(this._connectionString), GetSqlString,
                GetDvlSqlParameters);
        }

        private static IEnumerable<DvlSqlParameter> GetSqlParameters<TTuple>(TTuple[] @params, DvlSqlType[] types)
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
                            new object[] {param[i], types[i]});
                    var type2 = typeof(DvlSqlParameter<>).MakeGenericType(param[i].GetType());
                    string name = $"{types[i].Name}{count}";
                    yield return (DvlSqlParameter) Activator.CreateInstance(type2, new object[] {name, dvlSqlType});
                }

                count++;
            }
        }

        private string GetSqlString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this._insertExpression.Accept(commandBuilder);

            return builder.ToString();
        }

        private IEnumerable<DvlSqlParameter> GetDvlSqlParameters() => this._insertExpression.SqlParameters;
    }

    // ReSharper disable once IdentifierTypo
    public class SqlInsertable : IInsertable
    {
        private readonly DvlSqlInsertIntoSelectExpression _insertWithSelectExpression;
        private readonly string _connectionString;

        // ReSharper disable once IdentifierTypo
        public SqlInsertable(DvlSqlInsertIntoSelectExpression insertExpression, string connectionString) =>
            (this._insertWithSelectExpression, this._connectionString) = (insertExpression, connectionString);

        public IInsertDeleteExecutable SelectStatement(DvlSqlFullSelectExpression selectExpression, params DvlSqlParameter[] @params)
        {
            this._insertWithSelectExpression.SelectExpression = selectExpression;
            this._insertWithSelectExpression.Parameters = @params;

            return new SqlInsertDeleteExecutable(new DvlSqlConnection(this._connectionString), GetSqlString, GetDvlSqlParameters);
        }

        private IEnumerable<DvlSqlParameter> GetDvlSqlParameters() => this._insertWithSelectExpression.Parameters;

        private string GetSqlString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this._insertWithSelectExpression.Accept(commandBuilder);

            return builder.ToString();
        }
    }
}
