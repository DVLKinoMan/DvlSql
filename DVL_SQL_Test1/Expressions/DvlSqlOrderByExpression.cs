using System.Collections.Generic;
using System.Linq;
using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Expressions
{
    public enum Ordering
    {
        ASC,
        DESC
    }

    public class DvlSqlOrderByExpression : DvlSqlExpression
    {
        public List<(string column, Ordering ordering)> Params { get; set; }

        public DvlSqlOrderByExpression(IEnumerable<(string column, Ordering ordering)> @params) =>
            (this.Params, this.IsRoot) = (@params.ToList(), true);

        public void Add((string column, Ordering ordering) param) => this.Params.Add(param);

        public void AddRange(IEnumerable<(string column, Ordering ordering)> @params) => this.Params.AddRange(@params);

        public override void Accept(ISqlExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
