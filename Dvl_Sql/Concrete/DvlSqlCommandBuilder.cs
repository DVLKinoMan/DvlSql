using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Dvl_Sql.Abstract;
using Dvl_Sql.Expressions;
using Dvl_Sql.Models;
using static Dvl_Sql.Extensions.SystemExtensions;

namespace Dvl_Sql.Concrete
{
    internal class DvlSqlCommandBuilder : ISqlExpressionVisitor
    {
        private readonly StringBuilder _command;

        public DvlSqlCommandBuilder(StringBuilder command) => this._command = command;

        public void Visit(DvlSqlFromExpression expression)
        {
            this._command.TrimEnd(true);
            this._command.Append($"FROM {expression.TableName} ");
            if (expression.WithNoLock)
                this._command.Append("WITH(NOLOCK) ");
        }

        public void Visit(DvlSqlSelectExpression expression)
        {
            this._command.TrimEnd();
            this._command.Append(expression.IsRoot ? $"{Environment.NewLine}SELECT " : " SELECT ");

            if (expression.Top != null)
                this._command.Append($"TOP {expression.Top} ");

            if (expression.ParameterNames == null || !expression.ParameterNames.Any())
            {
                this._command.Append("* ");
                goto end;
            }

            bool isEmpty = true;
            foreach (var parameterName in expression.ParameterNames)
            {
                isEmpty = false;
                this._command.Append($"{parameterName}, ");
            }

            if (!isEmpty)
            {
                this._command.Remove(this._command.Length - 2, 2);
                this._command.Append(" ");
            }

            end:
            expression.FromExpression.Accept(this);
        }

        public void Visit(DvlSqlWhereExpression expression)
        {
            if (expression.InnerExpression is DvlSqlAndExpression andExp && !andExp.InnerExpressions.Any())
                return;

            this._command.TrimEnd();
            this._command.Append(expression.IsRoot ? $"{Environment.NewLine}WHERE " : " WHERE ");

            expression.InnerExpression.Accept(this);
        }

        public void Visit<TValue>(DvlSqlConstantExpression<TValue> expression) => this._command.Append(expression.StringValue);

        public void Visit(DvlSqlJoinExpression expression)
        {
            string joinCommand = expression switch
            {
                DvlSqlFullJoinExpression _ => "FULL OUTER JOIN ",
                DvlSqlInnerJoinExpression _ => "INNER JOIN ",
                DvlSqlLeftJoinExpression _ => "LEFT OUTER JOIN ",
                DvlSqlRightJoinExpression _ => "RIGHT OUTER JOIN ",
                _=>throw new NotImplementedException("JoinExpression not implemented")
            };

            this._command.TrimEnd();
            this._command.Append(expression.IsRoot ? $"{Environment.NewLine}{joinCommand} {expression.TableName} ON " : $" {joinCommand} {expression.TableName} ON ");
            expression.ComparisonExpression.Accept(this);
        }

        public void Visit(DvlSqlOrderByExpression expression)
        {
            this._command.TrimEnd();
            this._command.Append(expression.IsRoot ? $"{Environment.NewLine}ORDER BY " : " ORDER BY ");
            foreach (var (column, ordering) in expression.Params)
                this._command.Append($"{column} {ordering}, ");

            if (expression.Params.Count != 0)
                this._command.Remove(this._command.Length - 2, 2);
        }

        public void Visit(DvlSqlGroupByExpression expression)
        {
            this._command.TrimEnd();
            this._command.Append(expression.IsRoot ? $"{Environment.NewLine}GROUP BY " : " GROUP BY ");

            foreach (var parameterName in expression.ParameterNames)
                this._command.Append($"{parameterName}, ");

            if (expression.ParameterNames.Count != 0)
                this._command.Remove(this._command.Length - 2, 2);

            if (expression.BinaryExpression != null)
            {
                this._command.Append($"{Environment.NewLine}HAVING ");
                expression.BinaryExpression.Accept(this);
            }
        }

        public void Visit<TParam>(DvlSqlInsertIntoExpression<TParam> expression) where TParam : ITuple
        {
            this._command.TrimEnd();
            this._command.Append(expression.IsRoot ? $"{Environment.NewLine}INSERT INTO {expression.TableName}" : $" INSERT INTO {expression.TableName}");

            this._command.Append(" ( ");

            foreach (var column in expression.Columns)
                this._command.Append($"{column}, ");

            if (expression.Columns.Length != 0)
                this._command.Remove(this._command.Length - 2, 2);

            this._command.Append(" )");

            this._command.Append($"{Environment.NewLine}VALUES");
            int count = 0;
            foreach (var value in expression.Values)
            {
                this._command.Append($"{Environment.NewLine}( ");

                for (int i = 0; i < value.Length; i++, count++)
                    this._command.Append($"{expression.SqlParameters[count].Name}, ");

                this._command.Remove(this._command.Length - 2, 2);
                this._command.Append(" ),");
            }

            if (expression.Values.Any())
                this._command.Remove(this._command.Length - 1, 1);
        }

        public void Visit(DvlSqlInsertIntoSelectExpression expression)
        {
            this._command.TrimEnd();
            this._command.Append(expression.IsRoot ? $"{Environment.NewLine}INSERT INTO {expression.TableName}" : $" INSERT INTO {expression.TableName}");

            this._command.Append(" ( ");

            foreach (var column in expression.Columns)
                this._command.Append($"{column}, ");

            if (expression.Columns.Any())
                this._command.Remove(this._command.Length - 2, 2);

            this._command.Append(" )");

            expression.SelectExpression?.Accept(this);
        }

        public void Visit(DvlSqlFullSelectExpression expression)
        {
            expression.SqlSelectExpression.Accept(this);
            foreach (var joinExpression in expression.SqlJoinExpressions)
                joinExpression.Accept(this);
            expression.SqlWhereExpression?.Accept(this);
            expression.SqlGroupByExpression?.Accept(this);
            expression.SqlOrderByExpression?.Accept(this);
        }

        public void Visit(DvlSqlDeleteExpression expression)
        {
            this._command.Append($"DELETE ");
            expression.FromExpression.Accept(this);
            expression.WhereExpression?.Accept(this);
        }

        public void Visit(DvlSqlUpdateExpression expression)
        {
            this._command.Append($"UPDATE {expression.TableName}");
            this._command.Append($"{Environment.NewLine}SET ");
            foreach (var sqlParam in expression.DvlSqlParameters)
                this._command.Append($"{sqlParam.Name} = {sqlParam.Name.WithAlpha()}, ");

            this._command.Remove(this._command.Length - 2, 2);
            expression.WhereExpression?.Accept(this);
        }

        #region BinaryExpressions

        public void Visit(DvlSqlInExpression expression)
        {
            this._command.TrimEnd(true);
            this._command.Append($"{expression.ParameterName}{(expression.Not ? " NOT" : "")} IN (");

            bool isEmpty = true;

            foreach (var innerExpression in expression.InnerExpressions)
            {
                isEmpty = false;
                innerExpression.Accept(this);
                this._command.Append(", ");
            }

            if (!isEmpty)
                this._command.Remove(this._command.Length - 2, 2);

            this._command.Append(")");
        }

        public void Visit(DvlSqlOrExpression expression)
        {
            string op = expression.Not ? " AND " : " OR ";

            foreach (var innerExpression in expression.InnerExpressions)
            {
                innerExpression.Accept(this);
                this._command.TrimEnd();
                this._command.Append(op);
            }

            this._command.Remove(this._command.Length - op.Length, op.Length);
        }

        public void Visit(DvlSqlAndExpression expression)
        {
            string op = expression.Not ? " OR " : " AND ";

            foreach (var innerExpression in expression.InnerExpressions)
            {
                innerExpression.Accept(this);
                this._command.TrimEnd();
                this._command.Append(op);
            }

            this._command.Remove(this._command.Length - op.Length, op.Length);
        }

        public void Visit(DvlSqlComparisonExpression expression)
        {
            expression.LeftExpression.Accept(this);

            this._command.Append(
                (expression.Not ? GetNotExp(expression.ComparisonOperator) : expression.ComparisonOperator) switch
                {
                    SqlComparisonOperator.Equality => " = ",
                    SqlComparisonOperator.Greater => " > ",
                    SqlComparisonOperator.GreaterOrEqual => " >= ",
                    SqlComparisonOperator.Less => " < ",
                    SqlComparisonOperator.LessOrEqual => " <= ",
                    SqlComparisonOperator.NotEquality => " != ",
                    SqlComparisonOperator.Different => " <> ",
                    SqlComparisonOperator.NotGreater => " !< ",
                    SqlComparisonOperator.NotLess => " !> ",
                    _ => throw new NotImplementedException("ComparisonOperator not implemented")
                });

            expression.RightExpression.Accept(this);
            this._command.Append(" ");

            static SqlComparisonOperator GetNotExp(SqlComparisonOperator op) => op switch
            {
                SqlComparisonOperator.Different => SqlComparisonOperator.Equality,
                SqlComparisonOperator.Equality => SqlComparisonOperator.Different,
                SqlComparisonOperator.Less => SqlComparisonOperator.Greater,
                SqlComparisonOperator.Greater => SqlComparisonOperator.Less,
                SqlComparisonOperator.GreaterOrEqual => SqlComparisonOperator.LessOrEqual,
                SqlComparisonOperator.LessOrEqual => SqlComparisonOperator.GreaterOrEqual,
                SqlComparisonOperator.NotLess => SqlComparisonOperator.NotGreater,
                SqlComparisonOperator.NotGreater => SqlComparisonOperator.NotLess,
                SqlComparisonOperator.NotEquality => SqlComparisonOperator.Equality,
                _ => throw new NotImplementedException("ComparisonOperator not implemented")
            };
        }

        public void Visit(DvlSqlNotExpression expression)
        {
            expression.BinaryExpression.Not = !expression.Not;
            expression.BinaryExpression.Accept(this);
        }

        public void Visit(DvlSqlLikeExpression expression)
        {
            this._command.TrimEnd(true);
            string likeStr = expression.Not ? "NOT LIKE" : "LIKE";
            this._command.Append($"{expression.Field} {likeStr} '{expression.Pattern}' ");
        }

        public void Visit(DvlSqlIsNullExpression expression)
        {
            expression.Expression.Accept(this);
            this._command.Append(expression.Not ? " IS NOT NULL " : " IS NULL ");
        }

        #endregion

    }
}
