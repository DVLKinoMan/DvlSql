using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DVL_SQL_Test1
{
    public static class Functions
    {
        public static List<TResult> AsList<TResult>(SqlDataReader reader, Func<IDataRecord, TResult> recordSelector)
        {
            var list =new List<TResult>();
            while (reader.Read())
            {
                list.Add(recordSelector((IDataRecord)reader));
            }

            return list;
        }
    }
}
