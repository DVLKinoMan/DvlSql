using System;
using System.Collections.Generic;
using System.Text;
using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;

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
            expression.LeftExpression.Accept(this);

            this._command.Append(" OR ");

            expression.RightExpression.Accept(this);
        }

        public void Visit(DvlSqlAndExpression expression)
        {
            expression.LeftExpression.Accept(this);

            this._command.Append(" AND ");

            expression.RightExpression.Accept(this);
        }

        public void Visit(DvlSqlSelectExpression expression)
        {
            this._command.Append("SELECT ");

            if (expression.ParameterNames == null)
            {
                this._command.Append("*");
                return;
            }

            bool isEmpty = true;
            foreach (var parameterName in expression.ParameterNames)
            {
                isEmpty = false;
                this._command.Append($"{parameterName}, ");
            }

            if (!isEmpty)
                this._command.Remove(this._command.Length - 2, 2);

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

            this._command.Append(expression.ComparisonOperator switch
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
        }

        public void Visit<TValue>(DvlSqlConstantExpression<TValue> expression)
        {
            this._command.Append(expression.StringValue);
        }

        public void Visit(DvlSqlFromExpression expression)
        {
            this._command.Append($"FROM {expression.TableName}");
        }
    }
}
