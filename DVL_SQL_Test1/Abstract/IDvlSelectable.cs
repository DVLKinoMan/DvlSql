using DVL_SQL_Test1.Expressions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlSelectable
    {
        IExecutor Select(params string[] parameterNames);
        IExecutor Select();
        IDvlSelectable Where(DvlSqlWhereExpression whereExpression);
    }
}
