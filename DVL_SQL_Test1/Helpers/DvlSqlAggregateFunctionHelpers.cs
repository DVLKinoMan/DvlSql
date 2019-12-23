namespace Dvl_Sql.Helpers
{
    public static class DvlSqlAggregateFunctionHelpers
    {
        public static string AvgExp(string param) => $"AVG({param})";
        public static string CountExp(string param = "*") => $"COUNT({param})";
        public static string MaxExp(string param) => $"MAX({param})";
        public static string MinExp(string param) => $"MIN({param})";
        public static string SumExp(string param) => $"SUM({param})";
        public static string AsExp(string field, string @as) => $"{field} AS {@as}";
    }
}
