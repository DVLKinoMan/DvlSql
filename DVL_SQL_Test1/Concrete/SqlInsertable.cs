using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlInsertable<TParam> : IInsertable<TParam>
    {
        public IInsertExecutable Values(params TParam[] res)
        {
            throw new NotImplementedException();
        }

        public IInsertExecutable SelectStatement(DvlSqlSelectExpression selectExpression)
        {
            throw new NotImplementedException();
        }
    }
}
