using System.Collections.Generic;
using System.Linq;
using DvlSql.Abstract;

namespace DvlSql.Expressions
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

        public DvlSqlOrderByExpression(params (string column, Ordering ordering)[] @params) =>
            (this.Params, this.IsRoot) = (@params.ToList(), true);

        public void Add((string column, Ordering ordering) param) => this.Params.Add(param);

        public void AddRange(IEnumerable<(string column, Ordering ordering)> @params) => this.Params.AddRange(@params);

        // public DvlSqlOrderByExpression WithRoot(bool isRoot)
        // {
        //     this.IsRoot = isRoot;
        //     return this;
        // }

        public override void Accept(ISqlExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override DvlSqlExpression Clone() => OrderByClone();
            
        public DvlSqlOrderByExpression OrderByClone() => new DvlSqlOrderByExpression(Params.ToArray());
    }
}
