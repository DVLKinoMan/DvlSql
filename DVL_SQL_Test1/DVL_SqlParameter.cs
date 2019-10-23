using System;
using System.Data.SqlClient;

namespace DVL_SQL_Test1
{
    public class DVL_SqlParameter
    {
        private SqlParameter _sqlParameter;
        private Type _mapingType;

        public DVL_SqlParameter(SqlParameter parameter, Type mapType) =>
            (this._sqlParameter, this._mapingType)= (parameter, mapType);
    }
}
