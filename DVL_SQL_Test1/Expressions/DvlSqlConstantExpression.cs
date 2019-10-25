using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public class DvlSqlConstantExpression<TValue> : DvlSqlExpression
    {
        private TValue Value { get; }

        public string StringValue => this.Value.ToString();

        public DvlSqlConstantExpression(TValue value) => this.Value = value;

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
