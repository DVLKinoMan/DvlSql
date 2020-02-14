﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dvl_Sql.Abstract;
using Dvl_Sql.Expressions;
using Dvl_Sql.Models;

namespace Dvl_Sql.Concrete
{
    internal class SqlSelector : ISelector, IFilter, IGrouper, IUnionable, IFromable
    {
        private readonly IDvlSqlConnection _dvlSqlConnection;
        private readonly DvlSqlUnionExpression _unionExpression = new DvlSqlUnionExpression();
        
        private DvlSqlFullSelectExpression CurrFullSelectExpression => this._unionExpression.Last().Expression;

        public SqlSelector(DvlSqlFromExpression sqlFromExpression, IDvlSqlConnection dvlSqlConnection)
        {
            this._unionExpression.Add(new DvlSqlFullSelectExpression() {FromExpression = sqlFromExpression});
            this._dvlSqlConnection = dvlSqlConnection;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            var commandBuilder = new DvlSqlCommandBuilder(builder);

            this._unionExpression.Accept(commandBuilder);

            return builder.ToString();
        }

        public ISelector From(string tableName, bool withNoLock = false)
        {
            this.CurrFullSelectExpression.FromExpression = new DvlSqlFromExpression(tableName, withNoLock);

            return this;
        }

        public IEnumerable<DvlSqlParameter> GetDvlSqlParameters() => this.CurrFullSelectExpression?.WhereExpression?.Parameters;

        public SqlSelector WithSelectTop(int num)
        {
            if (this.CurrFullSelectExpression.SelectExpression != null)
                this.CurrFullSelectExpression.SelectExpression.Top = num;
            return this;
        }

        public IOrderer Select(params string[] parameterNames)
        {
            this.CurrFullSelectExpression.SelectExpression = new DvlSqlSelectExpression(this.CurrFullSelectExpression.FromExpression, parameterNames);

            return new SqlOrderer(this._dvlSqlConnection, this);
        }

        public IOrderer Select()
        {
            this.CurrFullSelectExpression.SelectExpression = new DvlSqlSelectExpression(this.CurrFullSelectExpression.FromExpression);

            return new SqlOrderer(this._dvlSqlConnection, this);
        }

        public IOrderer SelectTop(int count, params string[] parameterNames)
        {
            this.CurrFullSelectExpression.SelectExpression = new DvlSqlSelectExpression(this.CurrFullSelectExpression.FromExpression, parameterNames, count);

            return new SqlOrderer(this._dvlSqlConnection, this);
        }

        public IFilter Where(DvlSqlBinaryExpression binaryExpression)
        {
            this.CurrFullSelectExpression.WhereExpression = new DvlSqlWhereExpression(binaryExpression);

            return this;
        }

        public IFilter Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params)
        {
            this.CurrFullSelectExpression.WhereExpression = new DvlSqlWhereExpression(binaryExpression).WithParameters(@params) as DvlSqlWhereExpression;
            return this;
        }

        public ISelector Join(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this.CurrFullSelectExpression.JoinExpressions.Add(new DvlSqlInnerJoinExpression(tableName, compExpression));
            return this;
        }

        public ISelector Join(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            this.CurrFullSelectExpression.JoinExpressions.Add(new DvlSqlInnerJoinExpression(tableName,
                new DvlSqlConstantExpression<string>(firstTableMatchingCol) == new DvlSqlConstantExpression<string>(secondTableMatchingCol)));
            return this;
        }

        public ISelector FullJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this.CurrFullSelectExpression.JoinExpressions.Add(new DvlSqlFullJoinExpression(tableName, compExpression));
            return this;
        }

        public ISelector FullJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            this.CurrFullSelectExpression.JoinExpressions.Add(new DvlSqlFullJoinExpression(tableName,
                new DvlSqlConstantExpression<string>(firstTableMatchingCol) == new DvlSqlConstantExpression<string>(secondTableMatchingCol)));
            return this;
        }

        public ISelector LeftJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this.CurrFullSelectExpression.JoinExpressions.Add(new DvlSqlLeftJoinExpression(tableName, compExpression));
            return this;
        }

        public ISelector LeftJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            this.CurrFullSelectExpression.JoinExpressions.Add(new DvlSqlLeftJoinExpression(tableName,
                new DvlSqlConstantExpression<string>(firstTableMatchingCol) == new DvlSqlConstantExpression<string>(secondTableMatchingCol)));
            return this;
        }

        public ISelector RightJoin(string tableName, DvlSqlComparisonExpression compExpression)
        {
            this.CurrFullSelectExpression.JoinExpressions.Add(new DvlSqlRightJoinExpression(tableName, compExpression));
            return this;
        }

        public ISelector RightJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol)
        {
            this.CurrFullSelectExpression.JoinExpressions.Add(new DvlSqlRightJoinExpression(tableName,
                new DvlSqlConstantExpression<string>(firstTableMatchingCol) == new DvlSqlConstantExpression<string>(secondTableMatchingCol)));
            return this;
        }

        public IOrderer OrderBy(IOrderer orderBy, params string[] fields)
        {
            if (this.CurrFullSelectExpression.OrderByExpression == null)
                this.CurrFullSelectExpression.OrderByExpression = new DvlSqlOrderByExpression(fields.Select(f => (f, Ascending: Ordering.ASC)));
            else this.CurrFullSelectExpression.OrderByExpression.AddRange(fields.Select(f => (f, Ascending: Ordering.ASC)));

            return orderBy;
        }

        public IOrderer OrderByDescending(IOrderer orderBy, params string[] fields)
        {
            if (this.CurrFullSelectExpression.OrderByExpression == null)
                this.CurrFullSelectExpression.OrderByExpression = new DvlSqlOrderByExpression(fields.Select(f => (f, Descending: Ordering.DESC)));
            else this.CurrFullSelectExpression.OrderByExpression.AddRange(fields.Select(f => (f, Descending: Ordering.DESC)));

            return orderBy;
        }

        public IGrouper GroupBy(params string[] parameterNames)
        {
            this.CurrFullSelectExpression.GroupByExpression = new DvlSqlGroupByExpression(parameterNames);

            return this;
        }

        public ISelectable Having(DvlSqlBinaryExpression binaryExpression)
        {
            this.CurrFullSelectExpression.GroupByExpression.BinaryExpression = binaryExpression;

            return this;
        }

        public ISelectable Having(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params)
        {
            this.CurrFullSelectExpression.GroupByExpression.BinaryExpression = binaryExpression;
            this.CurrFullSelectExpression.GroupByExpression.WithParameters(@params);

            return this;
        }

        public IFromable Union()
        {
            var last = this._unionExpression.Last();
            this._unionExpression.RemoveAt(this._unionExpression.Count - 1);
            this._unionExpression.Add((last.Expression, UnionType.Union));
            this._unionExpression.Add(new DvlSqlFullSelectExpression());
            
            return this;
        }

        public IFromable UnionAll()
        {
            var last = this._unionExpression.Last();
            this._unionExpression.RemoveAt(this._unionExpression.Count - 1);
            this._unionExpression.Add((last.Expression, UnionType.UnionAll));
            this._unionExpression.Add(new DvlSqlFullSelectExpression());
            
            return this;
        }
    }
}
