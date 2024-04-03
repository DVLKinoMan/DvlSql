namespace DvlSql
{
    public class DvlSqlOptions(string connectionString)
    {
        public string ConnectionString { get; set; } = connectionString;
    }

}
