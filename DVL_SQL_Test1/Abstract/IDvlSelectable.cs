using DVL_SQL_Test1.Expressions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlSelectable
    {
        IExecuter Select(params string[] parameterNames);
        IExecuter Select();
        IDvlSelectable Where(DvlSqlWhereExpression whereExpression);
    }
}
