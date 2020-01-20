﻿using System.Data;
using Dvl_Sql.Models;

namespace Dvl_Sql.Extensions.Types
{
    public static partial class TypeHelpers
    {
        public static DvlSqlType<string> Xml(string name, string value) =>
            new DvlSqlType<string>(name, value, SqlDbType.Xml);

        public static DvlSqlType XmlType(string name) =>
            new DvlSqlType(name, SqlDbType.Xml);

        public static DvlSqlType XmlType() =>
            new DvlSqlType(SqlDbType.Xml);

        public static DvlSqlType<string> Xml(string value) =>
            new DvlSqlType<string>(value, SqlDbType.Xml);
    }
}
