namespace DvlSql
{
    public class DvlSqlOptions
    {
        public string ConnectionString { get; set; }

        public DvlSqlOptions(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }

}
