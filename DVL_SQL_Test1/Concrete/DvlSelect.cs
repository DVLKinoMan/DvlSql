﻿using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSelect : IDvlSelect
    {
        private readonly DvlSqlFromExpression _sqlFromExpression;
        private DvlSqlWhereExpression _sqlWhereExpression;
        private readonly List<DvlSqlJoinExpression> _sqlJoinExpressions = new List<DvlSqlJoinExpression>();

        private DvlSqlSelectExpression _sqlSelectExpression;
        private DvlSqlOrderByExpression _sqlOrderByExpression;
        private readonly string _connectionString;

        public DvlSelect(DvlSqlFromExpression sqlFromExpression, string connectionString)
            => (this._sqlFromExpression, this._connectionString) = (sqlFromExpression, connectionString);

        public string GetSqlString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this._sqlSelectExpression.Accept(commandBuilder);
            foreach (var joinExpression in this._sqlJoinExpressions)
                joinExpression.Accept(commandBuilder);
            this._sqlWhereExpression?.Accept(commandBuilder);
            this._sqlOrderByExpression?.Accept(commandBuilder);

            return builder.ToString();
        }

        public DvlSelect WithSelectTop(int num)
        {
            if (this._sqlSelectExpression != null)
                this._sqlSelectExpression.Top = num;
            return this;
        }

        public IDvlSqlExecutor Select(params string[] parameterNames)
        {
            this._sqlSelectExpression = new DvlSqlSelectExpression(this._sqlFromExpression, parameterNames);

            return new DvlSqlExecutor(new DvlSqlConnection(this._connectionString), this);
        }

        public IDvlSqlExecutor Select()
        {
            this._sqlSelectExpression = new DvlSqlSelectExpression(this._sqlFromExpression);

            return new DvlSqlExecutor(new DvlSqlConnection(this._connectionString), this);
        }


        public IDvlSqlExecutor SelectTop(int count, params string[] parameterNames)
        {
            this._sqlSelectExpression = new DvlSqlSelectExpression(this._sqlFromExpression, parameterNames, count);

            return new DvlSqlExecutor(new DvlSqlConnection(this._connectionString), this);
        }

        public IDvlWhere Where(DvlSqlBinaryExpression binaryExpression)
        {
            this._sqlWhereExpression = new DvlSqlWhereExpression(binaryExpression);
            return new DvlWhere(this);
        }

        public IDvlSelect Join(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlInnerJoinExpression(tableName, compExpression));
            return this;
        }

        public IDvlSelect FullJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlInnerJoinExpression(tableName, compExpression));
            return this;
        }

        public IDvlSelect LeftJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlLeftJoinExpression(tableName, compExpression));
            return this;
        }

        public IDvlSelect RightJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this._sqlJoinExpressions.Add(new DvlSqlRightJoinExpression(tableName, compExpression));
            return this;
        }

        public IDvlOrderBy OrderBy(params string[] fields)
        {

            if (this._sqlOrderByExpression == null)
                this._sqlOrderByExpression = new DvlSqlOrderByExpression(fields.Select(f => (f, Ascending: Ordering.ASC)));
            else this._sqlOrderByExpression.AddRange(fields.Select(f => (f, Ascending: Ordering.ASC)));

            return new DvlOrderBy(this);
        }

        public IDvlOrderBy OrderByDescending(params string[] fields)
        {

            if (this._sqlOrderByExpression == null)
                this._sqlOrderByExpression = new DvlSqlOrderByExpression(fields.Select(f => (f, Descending: Ordering.DESC)));
            else this._sqlOrderByExpression.AddRange(fields.Select(f => (f, Descending: Ordering.DESC)));

            return new DvlOrderBy(this);
        }
    }
}
