//using System.Data.SqlClient;


namespace DvlSql.Models
{
    public abstract class DvlSqlParameter
    {
        public string Name { get; set; }
        public DvlSqlType Type { get; set; }

        protected DvlSqlParameter(string name, DvlSqlType type)
        {
            Name = name.WithAlpha();
            Type = type;
        }
    }
}
