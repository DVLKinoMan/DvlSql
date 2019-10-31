﻿namespace DVL_SQL_Test1.Abstract
{
    public interface ISelectable
    {
        IOrderer Select(params string[] parameterNames);
        IOrderer Select();
        IOrderer SelectTop(int count, params string[] parameterNames);
    }
}
