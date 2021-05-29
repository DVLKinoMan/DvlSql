using System.Collections.Generic;
using Dvl_Sql.Abstract;
using Dvl_Sql.Models;

namespace Dvl_Sql.Expressions
{
    public class DvlSqlUpdateExpression : DvlSqlExpressionWithParameters
    {
        public string TableName { get; set; }
        public DvlSqlWhereExpression WhereExpression { get; set; }
        public List<DvlSqlParameter> DvlSqlParameters { get; set; } = new List<DvlSqlParameter>();

        public DvlSqlUpdateExpression(string tableName) =>
            this.TableName = tableName;

        public void Add<TVal>(DvlSqlType<TVal> val)
        {
            this.DvlSqlParameters.Add(new DvlSqlParameter<TVal>(val.Name, val));
        }

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
        public override DvlSqlExpression Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
