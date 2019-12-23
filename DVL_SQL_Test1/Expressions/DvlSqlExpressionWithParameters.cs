using System.Collections.Generic;
using Dvl_Sql.Models;

namespace Dvl_Sql.Expressions
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
