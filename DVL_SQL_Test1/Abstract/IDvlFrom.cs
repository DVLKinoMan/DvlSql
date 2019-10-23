using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DVL_SQL_Test1.Abstract
{
    public interface IDvlFrom
    {
        IDvlSelectable From(string tableName);
    }
}
