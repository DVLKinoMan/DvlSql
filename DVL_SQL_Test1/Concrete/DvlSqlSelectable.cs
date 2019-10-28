﻿using System;
using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSqlSelectable : IDvlSelectable
    {
        private readonly DvlSqlFromExpression _sqlFromExpression;
        private readonly List<DvlSqlWhereExpression> _sqlWhereExpressions = new List<DvlSqlWhereExpression>();
        private readonly List<DvlSqlJoinExpression> _sqlJoinExpressions = new List<DvlSqlJoinExpression>();
        //private DvlSqlOrderByExpression _sqlOrderByExpression;
        private readonly string _connectionString;

        public Func<DvlSqlOrderByExpression, StringBuilder> func;

        public DvlSqlSelectable(DvlSqlFromExpression sqlFromExpression, string connectionString)
            => (this._sqlFromExpression, this._connectionString) = (sqlFromExpression, connectionString);

        public IExecutor Select(params string[] parameterNames)
        {
            func = (DvlSqlOrderByExpression orderByExpression) =>
            {
                var builder = new StringBuilder();
                var commandBuilder = new DvlSqlCommandBuilder(builder);

                var selectExpression = new DvlSqlSelectExpression(this._sqlFromExpression, parameterNames);
                var whereExpression = GetOneWhereExpression(this._sqlWhereExpressions);

                selectExpression.Accept(commandBuilder);
                foreach (var joinExpression in this._sqlJoinExpressions)
                    joinExpression.Accept(commandBuilder);
                whereExpression.Accept(commandBuilder);
                orderByExpression?.Accept(commandBuilder);

                return builder;
            };

            return new SqlExecutor(new DvlSqlConnection(this._connectionString), this);
        }

        public IExecutor Select()
        {
            func = (DvlSqlOrderByExpression orderByExpression) =>
            {
                var builder = new StringBuilder();
                var commandBuilder = new DvlSqlCommandBuilder(builder);

                var selectExpression = new DvlSqlSelectExpression(this._sqlFromExpression);
                var whereExpression = GetOneWhereExpression(this._sqlWhereExpressions);

                selectExpression.Accept(commandBuilder);
                foreach (var joinExpression in this._sqlJoinExpressions)
                    joinExpression.Accept(commandBuilder);
                whereExpression.Accept(commandBuilder);
                orderByExpression?.Accept(commandBuilder);

                return builder;
            };
            
            return new SqlExecutor(new DvlSqlConnection(this._connectionString), this);
        }

        public IDvlSelectable Where(DvlSqlBinaryExpression binaryExpression)
        {
            this._sqlWhereExpressions.Add(new DvlSqlWhereExpression(binaryExpression));
            return this;
        }

        public IDvlSelectable Join(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlInnerJoinExpression(tableName, compExpression));
            return this;
        }

        public IDvlSelectable FullJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlInnerJoinExpression(tableName, compExpression));
            return this;
        }

        public IDvlSelectable LeftJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlLeftJoinExpression(tableName, compExpression));
            return this;
        }

        public IDvlSelectable RightJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlRightJoinExpression(tableName, compExpression));
            return this;
        }

        private static DvlSqlWhereExpression
            GetOneWhereExpression(IEnumerable<DvlSqlWhereExpression> whereExpressions) =>
            new DvlSqlWhereExpression(new DvlSqlAndExpression(whereExpressions.Select(ex => ex.InnerExpression)));

    }
}
