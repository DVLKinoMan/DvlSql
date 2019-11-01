using System.Globalization;

namespace DVL_SQL_Test1.Helpers
{
    public static class DvlSqlHelpers
    {
        public static string GetDefaultSqlString<TValue>(TValue value) =>
            value switch
            {
                string str => $"'{str}'",
                int i => i.ToString(),
                char ch => $"'{ch}'",
                decimal d => d.ToString(CultureInfo.InvariantCulture),
                _ => value.ToString()
            };
    }
}
