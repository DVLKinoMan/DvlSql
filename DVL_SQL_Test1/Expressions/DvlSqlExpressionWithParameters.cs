using DVL_SQL_Test1.Models;
using System.Collections.Generic;

namespace DVL_SQL_Test1.Expressions
{
    public abstract class DvlSqlExpressionWithParameters: DvlSqlExpression
    {
        public IEnumerable<DvlSqlParameter> Parameters { get; set; } = new List<DvlSqlParameter>();

        public DvlSqlExpressionWithParameters WithParameters(IEnumerable<DvlSqlParameter> @params)
        {
            this.Parameters = @params;
            return this;
        }
    }
}
