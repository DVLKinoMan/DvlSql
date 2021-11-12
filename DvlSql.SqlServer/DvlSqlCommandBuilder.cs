using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DvlSql.Expressions;
using static System.Extensions.Exts;

namespace DvlSql.SqlServer
{
    internal class DvlSqlCommandBuilder : ISqlExpressionVisitor
    {
        private readonly StringBuilder _command;

        public DvlSqlCommandBuilder(StringBuilder command) => this._command = command;

        public void Visit(DvlSqlFromExpression expression)
        {
            switch (expression)
            {
                case DvlSqlFromWithTableExpression { } fromWithTable:
                    this._command.Append($"FROM {fromWithTable.TableName}");
                    expression.As?.Accept(this);
                    if (fromWithTable.WithNoLock)
                        this._command.Append(" WITH(NOLOCK)");
                    break;
                default:
                    this._command.Append("FROM (");
                    if (expression is DvlSqlFullSelectExpression { } select)
                        Visit(select);
                    else expression.Accept(this);
                    this._command.Append(") ");
                    expression.As?.Accept(this);
                    break;
            }
        }

        public void Visit(DvlSqlSelectExpression expression)
        {
            this._command.Append("SELECT ");

            if (expression.Top != null)
                this._command.Append($"TOP {expression.Top} ");

            if (expression.ParameterNames == null || !expression.ParameterNames.Any())
            {
                this._command.Append("* ");
                //goto end;
                return;
            }

            this._command.Append(string.Join(", ", expression.ParameterNames.Where(p=>!string.IsNullOrEmpty(p))));
            this._command.Append(" ");

            //end:
            //expression.From.Accept(this);
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
            this._command.Append(expression.StringValue.GetEscapedString(false));

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
            expression.OutputExpression?.IntoTable?.Accept(this);

            this._command.Append($"INSERT INTO {expression.TableName}");

            this._command.Append(" ( ");

            foreach (var column in expression.Columns)
                this._command.Append($"{column}, ");

            if (expression.Columns.Length != 0)
                this._command.Remove(this._command.Length - 2, 2);

            this._command.Append(" )");

            expression.OutputExpression?.Accept(this);
            expression.ValuesExpression?.Accept(this);
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

            expression.OutputExpression?.Accept(this);

            expression.SelectExpression?.Accept(this);
            this._command.TrimEnd();
        }

        public void Visit(DvlSqlFullSelectExpression expression)
        {
            if (expression.Select == null)
                throw new ArgumentNullException("SelectExpression", "expression has no Select Expression");

            expression.Select.Accept(this);
            expression.From.Accept(this);
            foreach (var joinExpression in expression.Join)
                joinExpression.Accept(this);
            expression.Where?.Accept(this);
            expression.GroupBy?.Accept(this);
            expression.OrderBy?.Accept(this);
            expression.Skip?.Accept(this);
        }

        public void Visit(DvlSqlDeleteExpression expression)
        {
            this._command.Append(
                $"DELETE {(expression.FromExpression.As != null ? $"{expression.FromExpression.As.Name}" : expression.Join?.Count != 0 ? expression.FromExpression.TableName : "")} ");
            expression.FromExpression.Accept(this);
            expression.OutputExpression?.Accept(this);
            if (expression.Join?.Count != 0)
            {
                if (expression.OutputExpression != null)
                    throw new Exception("Joins can not be when Output Expression exists");
                expression.Join?.ForEach(j => j.Accept(this));
            }

            expression.WhereExpression?.Accept(this);
        }

        public void Visit(DvlSqlUpdateExpression expression)
        {
            this._command.Append($"UPDATE {expression.TableName}");
            this._command.Append($"{Environment.NewLine}SET ");
            foreach (var sqlParam in expression.DvlSqlParameters)
                this._command.Append(
                    sqlParam switch
                    {
                        DvlSqlParameter<string> {ExactValue: true} stringParam =>
                            $"{stringParam.Name} = {stringParam.Value}, ",
                        _ => $"{sqlParam.Name} = {sqlParam.Name.WithAlpha()}, "
                    });

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

        public void Visit(DvlSqlTableDeclarationExpression expression)
        {
            this._command.Append($"{Environment.NewLine}DECLARE {expression.TableName} TABLE (");

            foreach (var col in expression.Columns)
            {
                this._command.Append(
                    $"{Environment.NewLine}{col.Name} {col.SqlDbType}{(col.Size != null ? $"({col.Size})" : "")} {(col.IsNotNull ? "NOT NULL" : "NULL")},");
            }

            if (expression.Columns.Count > 0)
                this._command.Remove(this._command.Length - 1, 1);

            this._command.Append(");");
        }

        public void Visit(DvlSqlOutputExpression expression)
        {
            this._command.Append($"{Environment.NewLine}OUTPUT {string.Join(',', expression.Columns)}");
            if (expression.IntoTable != null)
                this._command.Append($"{Environment.NewLine}INTO {expression.IntoTable.TableName}");
            this._command.Append(Environment.NewLine);
        }

        public void Visit<T>(DvlSqlValuesExpression<T> expression) where T : ITuple
        {
            bool hasAs = expression.As != null;
            this._command.Append($"{Environment.NewLine}{(hasAs ? $"FROM{Environment.NewLine}(" : "")}VALUES");
            int count = 0;
            int? len = null;
            foreach (var value in expression.Values)
            {
                this._command.Append($"{Environment.NewLine}( ");
                
                for (int i = 0; i < GetLen(value); i++, count++)
                    this._command.Append($"{expression.SqlParameters[count].Name}, ");

                this._command.Remove(this._command.Length - 2, 2);
                this._command.Append(" ),");
            }

            if (expression.Values.Any())
                this._command.Remove(this._command.Length - 1, 1);
            this._command.Append(hasAs ? ")" : "");

            expression.As?.Accept(this);

            int GetLen(ITuple value)
            {
                if (len is {} i)
                    return i;

                return (int)(len = value.Length == 8 && value[7] is ITuple tup ? 7 + GetLen(tup) : value.Length);
            }
        }

        public void Visit(DvlSqlAsExpression expression)
        {
            this._command.Append($" {(expression.UseAsKeyword ? "AS" : "")} {expression.Name}");
            if (expression.Parameters != null)
                this._command.Append($"({string.Join(", ", expression.Parameters)})");
        }

        #region BinaryExpressions

        public void Visit(DvlSqlInExpression expression)
        {
            this._command.Append($"{expression.ParameterName}{(expression.Not ? " NOT" : "")} IN ( ");

            bool isEmpty = true;

            foreach (var innerExpression in expression.InnerExpressions)
            {
                if(innerExpression is DvlSqlBinaryEmptyExpression)
                    continue;
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
                if (innerExpression is DvlSqlBinaryEmptyExpression)
                    continue;
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
                if(innerExpression is DvlSqlBinaryEmptyExpression)
                    continue;
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

        public void Visit(DvlSqlLikeExpression expression)
        {
            string likeStr = expression.Not ? "NOT LIKE" : "LIKE";
            this._command.Append($"{expression.Field} {likeStr} '{expression.Pattern.GetEscapedString()}'");
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

        public void Visit(DvlSqlBinaryEmptyExpression expression)
        {
        }
        #endregion
    }
}