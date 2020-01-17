using System;
using System.Data;
using Dvl_Sql.Models;

namespace Dvl_Sql.Extensions.Types
{
    public static partial class TypeHelpers
    {
        public static DvlSqlType<Guid> UniqueIdentifier(string name, Guid value) =>
            new DvlSqlType<Guid>(name, value, SqlDbType.UniqueIdentifier);

        public static DvlSqlType UniqueIdentifierType(string name) =>
            new DvlSqlType(name, SqlDbType.UniqueIdentifier);

        public static DvlSqlType<Guid> UniqueIdentifier(Guid value) =>
            new DvlSqlType<Guid>(value, SqlDbType.UniqueIdentifier);
    }
}
