namespace DVL_SQL_Test1.Extensions
{
    public static class DvlSqlExtensions
    {
        public static string As(this string field, string @as) => $"{field} AS {@as}";
    }
}
