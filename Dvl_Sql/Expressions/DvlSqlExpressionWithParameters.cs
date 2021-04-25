using System.Collections.Generic;
using System.Linq;
using Dvl_Sql.Models;

namespace Dvl_Sql.Expressions
{
    public abstract class DvlSqlExpressionWithParameters: DvlSqlExpression
    {
        public List<DvlSqlParameter> Parameters { get; set; } = new List<DvlSqlParameter>();

        public DvlSqlExpressionWithParameters WithParameters(IEnumerable<DvlSqlParameter> @params)
        {
            this.Parameters = @params.ToList();
            return this;
        }
    }
}
