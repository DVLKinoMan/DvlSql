namespace DvlSql
{
    public class DvlSqlOptions
    {
        public string ConnectionString { get; set; } = default!;

        public DvlSqlOptions() { }

        public DvlSqlOptions(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }

}
