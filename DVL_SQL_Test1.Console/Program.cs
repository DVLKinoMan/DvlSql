using System;
using System.Data;
using System.Data.SqlClient;
using DVL_SQL_Test1.Concrete;
using DVL_SQL_Test1.Expressions;
using static DVL_SQL_Test1.Functions;

namespace DVL_SQL_Test1.Console
{
    class Program
    {
        public class Cl
        {
            public byte Status;
            public decimal Amount;
            public string RestrictCode;

            public override string ToString()
            {
                return $"{Status} {Amount} {RestrictCode}";
            }
        }

        static void Main(string[] args)
        {
            string connString = "Data Source=SQL; Initial Catalog=BANK2000; Connection Timeout=30; User Id=b2000; Password=1234; Application Name = CoreApi";

            var list = new DvlSql(connString)
                .From("nbe.BANK_DATA")
                .Where(new DvlSqlWhereExpression(
                    new DvlSqlAndExpression(
                    new DvlSqlComparisonExpression(new DvlSqlConstantExpression<string>("AMOUNT"), SqlComparisonOperator.Less, new DvlSqlConstantExpression<int>(350000))
                    //new DvlSqlComparisonExpression(new DvlSqlConstantExpression<string>("ADD_DATE"), SqlComparisonOperator.Less, new DvlSqlConstantExpression<DateTime>(new DateTime(2012, 1, 1)))
                    ))
                )
                .Select("STATUS", "AMOUNT", "RESTRICT_CODE")
                .ToListAsync(r=> new Cl
                {
                    Status = (byte)r[0],
                    Amount = (decimal)r[1],
                    RestrictCode = (string)r[2]
                }).Result;

            foreach (var l in list)
            {
                System.Console.WriteLine(l);
            }
            System.Console.WriteLine("Hello World!");
        }
    }
}
