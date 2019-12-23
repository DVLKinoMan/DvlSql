﻿using Dvl_Sql.Models;

namespace Dvl_Sql.Abstract
{
    public interface IUpdateSetable
    {
        IUpdateable Set<TVal>((string, DvlSqlType<TVal>) value);
    }
}
