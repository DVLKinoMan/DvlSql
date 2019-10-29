using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;
using System;
using System.Text;

namespace DVL_SQL_Test1.Concrete
{
    public class DvlSqlCommandBuilder : ISqlExpressionVisitor
    {
        private readonly StringBuilder _command;

        public DvlSqlCommandBuilder(StringBuilder command) => this._command = command;

        public void Visit(DvlSqlInExpression expression)
        {
            this._command.Append($"{expression.ParameterName} IN (");

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
            const string or = " OR ";

            foreach (var innerExpression in expression.InnerExpressions)
            {
                innerExpression.Accept(this);
                this._command.Append(or);
            }

            this._command.Remove(this._command.Length - or.Length, or.Length);
        }

        public void Visit(DvlSqlAndExpression expression)
        {
            const string and = " AND ";

            foreach (var innerExpression in expression.InnerExpressions)
            {
                innerExpression.Accept(this);
                this._command.Append(and);
            }

            this._command.Remove(this._command.Length - and.Length, and.Length);
        }

        public void Visit(DvlSqlSelectExpression expression)
        {
            this._command.Append("SELECT ");

            if (expression.ParameterNames == null)
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
            this._command.Append("WHERE ");

            expression.InnerExpression.Accept(this);
        }

        public void Visit(DvlSqlComparisonExpression expression)
        {
            expression.LeftExpression.Accept(this);

            this._command.Append(
                expression.ComparisonOperator switch
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
        }

        public void Visit<TValue>(DvlSqlConstantExpression<TValue> expression) => this._command.Append(expression.StringValue);

        public void Visit(DvlSqlFromExpression expression)
        {
            this._command.Append($"FROM {expression.TableName} ");
            if (expression.WithNoLock)
                this._command.Append("WITH(NOLOCK) ");
        }

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

            this._command.Append($"{joinCommand} {expression.TableName} ");
            this._command.Append("ON ");
            expression.ComparisonExpression.Accept(this);
        }

        public void Visit(DvlSqlOrderByExpression expression)
        {
            this._command.Append("ORDER BY ");
            foreach (var (column, ordering) in expression.Params)
                this._command.Append($"{column} {ordering}, ");

            if (expression.Params.Count != 0)
                this._command.Remove(this._command.Length - 2, 2);
        }
    }
}
