using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Dvl_Sql.Abstract;
using Dvl_Sql.Expressions;
using static Dvl_Sql.Helpers.SystemExtensions;

namespace Dvl_Sql.Concrete
{
    internal class DvlSqlCommandBuilder : ISqlExpressionVisitor
    {
        private readonly StringBuilder _command;

        public DvlSqlCommandBuilder(StringBuilder command) => this._command = command;

        public void Visit(DvlSqlFromExpression expression)
        {
            this._command.Append($"FROM {expression.TableName}");
            if (expression.WithNoLock)
                this._command.Append(" WITH(NOLOCK)");
        }

        public void Visit(DvlSqlSelectExpression expression)
        {
            this._command.Append("SELECT ");

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
                if (string.IsNullOrEmpty(parameterName))
                    throw new ArgumentException("One of the parameters was null or empty",
                        "DvlSqlSelectExpression.ParameterNames");
                isEmpty = false;
                this._command.Append($"{parameterName}, ");
            }

            if (!isEmpty)
            {
                this._command.Remove(this._command.Length - 2, 2);
                this._command.Append(" ");
            }

            end:
            expression.From.Accept(this);
        }

        public void Visit(DvlSqlWhereExpression expression)
        {
            switch (expression.InnerExpression)
            {
                case DvlSqlAndExpression andExp when !andExp.InnerExpressions.Any():
                case DvlSqlOrExpression orExp when !orExp.InnerExpressions.Any():
                    return;
                default:
                    this._command.Append(expression.IsRoot ? $"{Environment.NewLine}WHERE " : " WHERE ");

                    expression.InnerExpression.Accept(this);
                    break;
            }
        }

        public void Visit<TValue>(DvlSqlConstantExpression<TValue> expression) =>
            this._command.Append(expression.StringValue);

        public void Visit(DvlSqlJoinExpression expression)
        {
            string joinCommand = expression switch
            {
                DvlSqlFullJoinExpression _ => "FULL OUTER JOIN",
                DvlSqlInnerJoinExpression _ => "INNER JOIN",
                DvlSqlLeftJoinExpression _ => "LEFT OUTER JOIN",
                DvlSqlRightJoinExpression _ => "RIGHT OUTER JOIN",
                _ => throw new NotImplementedException("JoinExpression not implemented")
            };

            this._command.Append(expression.IsRoot
                ? $"{Environment.NewLine}{joinCommand} {expression.TableName} ON "
                : $" {joinCommand} {expression.TableName} ON ");
            expression.ComparisonExpression.Accept(this);
        }

        public void Visit(DvlSqlOrderByExpression expression)
        {
            this._command.Append(expression.IsRoot ? $"{Environment.NewLine}ORDER BY " : " ORDER BY ");
            foreach (var (column, ordering) in expression.Params)
                this._command.Append($"{column} {ordering}, ");

            if (expression.Params.Count != 0)
                this._command.Remove(this._command.Length - 2, 2);
        }

        public void Visit(DvlSqlGroupByExpression expression)
        {
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
            this._command.Append($"INSERT INTO {expression.TableName}");

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
            this._command.Append($"INSERT INTO {expression.TableName}");

            this._command.Append(" ( ");

            foreach (var column in expression.Columns)
                this._command.Append($"{column}, ");

            if (expression.Columns.Any())
                this._command.Remove(this._command.Length - 2, 2);

            this._command.Append(" ) ");

            expression.SelectExpression?.Accept(this);
            this._command.TrimEnd();
        }

        public void Visit(DvlSqlFullSelectExpression expression)
        {
            if (expression.Select == null)
                throw new ArgumentNullException("SelectExpression", "expression has no Select Expression");

            expression.Select.Accept(this);
            foreach (var joinExpression in expression.Join)
                joinExpression.Accept(this);
            expression.Where?.Accept(this);
            expression.GroupBy?.Accept(this);
            expression.OrderBy?.Accept(this);
            expression.Skip?.Accept(this);
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

        public void Visit(DvlSqlUnionExpression expression)
        {
            foreach (var (selectExpression, type) in expression)
            {
                selectExpression.Accept(this);
                if (type != null)
                    this._command.AppendLine(
                        $"{Environment.NewLine}{(type == UnionType.Union ? "UNION" : "UNION ALL")}");
            }
        }

        public void Visit(DvlSqlSkipExpression expression)
        {
            this._command.Append($"{Environment.NewLine}OFFSET {expression.OffsetRows} ROWS");
            if (expression.FetchNextRows != null)
                this._command.Append($" FETCH NEXT {expression.FetchNextRows} ROWS ONLY");
        }
        
        #region BinaryExpressions

        public void Visit(DvlSqlInExpression expression)
        {
            this._command.Append($"{expression.ParameterName}{(expression.Not ? " NOT" : "")} IN ( ");

            bool isEmpty = true;

            foreach (var innerExpression in expression.InnerExpressions)
            {
                isEmpty = false;
                innerExpression.Accept(this);
                this._command.Append(", ");
            }

            if (!isEmpty)
                this._command.Remove(this._command.Length - 2, 2);

            this._command.Append(" )");
        }

        public void Visit(DvlSqlOrExpression expression)
        {
            string op = expression.Not ? " AND " : " OR ";

            this._command.TrimIfLastCharacterIs('(');
            this._command.Append("( ");
            foreach (var innerExpression in expression.InnerExpressions)
            {
                innerExpression.Accept(this);
                this._command.Append(op);
            }

            this._command.Remove(this._command.Length - op.Length, op.Length);
            this._command.Append(this._command[^1] == ')'? ")" : " )");
        }

        public void Visit(DvlSqlAndExpression expression)
        {
            string op = expression.Not ? " OR " : " AND ";

            this._command.TrimIfLastCharacterIs('(');
            this._command.Append("( ");
            foreach (var innerExpression in expression.InnerExpressions)
            {
                innerExpression.Accept(this);
                this._command.Append(op);
            }

            this._command.Remove(this._command.Length - op.Length, op.Length);
            this._command.Append(this._command[^1] == ')'? ")" : " )");
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

            static SqlComparisonOperator GetNotExp(SqlComparisonOperator op) => op switch
            {
                SqlComparisonOperator.Different => SqlComparisonOperator.Equality,
                SqlComparisonOperator.Equality => SqlComparisonOperator.Different,
                SqlComparisonOperator.Less => SqlComparisonOperator.GreaterOrEqual,
                SqlComparisonOperator.Greater => SqlComparisonOperator.LessOrEqual,
                SqlComparisonOperator.GreaterOrEqual => SqlComparisonOperator.Less,
                SqlComparisonOperator.LessOrEqual => SqlComparisonOperator.Greater,
                SqlComparisonOperator.NotLess => SqlComparisonOperator.GreaterOrEqual,
                SqlComparisonOperator.NotGreater => SqlComparisonOperator.LessOrEqual,
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
            string likeStr = expression.Not ? "NOT LIKE" : "LIKE";
            this._command.Append($"{expression.Field} {likeStr} '{expression.Pattern}'");
        }

        public void Visit(DvlSqlIsNullExpression expression)
        {
            expression.Expression.Accept(this);
            this._command.Append(expression.Not ? " IS NOT NULL" : " IS NULL");
        }
        
        public void Visit(DvlSqlExistsExpression expression)
        {
            this._command.Append($"{(expression.Not ? "NOT " : "")}EXISTS( ");
            expression.Select.Accept(this);
            this._command.Append(" )");
        }

        #endregion
    }
}