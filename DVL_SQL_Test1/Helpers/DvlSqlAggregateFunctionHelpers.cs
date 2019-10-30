namespace DVL_SQL_Test1.Helpers
{
    public static class DvlSqlAggregateFunctionHelpers
    {
        public static string Avg(string param) => $"AVG({param})";
        public static string Count(string param = "*") => $"COUNT({param})";
        public static string Max(string param) => $"MAX({param})";
        public static string Min(string param) => $"MIN({param})";
        public static string Sum(string param) => $"SUM({param})";
    }
}
