using System.Collections.Generic;
using System.Linq;
using Dvl_Sql.Abstract;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlGroupByExpression : DvlSqlExpressionWithParameters
    {
        public List<string> ParameterNames;
        public DvlSqlBinaryExpression BinaryExpression { get; set; }

        public DvlSqlGroupByExpression(IEnumerable<string> parameterNames) => (this.ParameterNames, this.IsRoot) = (parameterNames.ToList(), true);

        // public DvlSqlGroupByExpression WithRoot(bool isRoot)
        // {
        //     this.IsRoot = isRoot;
        //     return this;
        // }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    }
}
