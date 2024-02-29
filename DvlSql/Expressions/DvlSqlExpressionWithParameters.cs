using System.Collections.Generic;
using System.Linq;

namespace DvlSql.Expressions
{
    public abstract class DvlSqlExpressionWithParameters: DvlSqlExpression
    {
        public List<DvlSqlParameter> Parameters { get; set; } = [];

        public DvlSqlExpressionWithParameters WithParameters(IEnumerable<DvlSqlParameter> @params)
        {
            this.Parameters = @params.ToList();
            return this;
        }
    }
}
