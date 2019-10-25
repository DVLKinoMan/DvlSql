using System;
using System.Data;
using System.Data.SqlClient;
using DVL_SQL_Test1.Concrete;
using DVL_SQL_Test1.Expressions;
using static DVL_SQL_Test1.Helpers.DvlSqlExpressionHelpers;

namespace DVL_SQL_Test1.Console
{
    class Program
    {
        public class Cl
        {
            public byte Status;
            public decimal Amount;
            public string RestrictCode;

            public override string ToString() => $"{this.Status} {this.Amount} {this.RestrictCode}";
        }

        static void Main()
        {
            string connString = "Data Source=SQL; Initial Catalog=BANK2000; Connection Timeout=30; User Id=b2000; Password=1234; Application Name = CoreApi";

            var list = new DvlSql(connString)
                .From("nbe.BANK_DATA", true)
                .Where(
                        AndExp(
                            ComparisonExp(ConstantExp("AMOUNT"), SqlComparisonOperator.Less, ConstantExp(350000)),
                            //ComparisonExp(ConstantExp("ADD_DATE"), SqlComparisonOperator.Less, ConstantExp(new DateTime(2012, 1, 1)))
                            ComparisonExp(ConstantExp("STATUS"), SqlComparisonOperator.Equality, ConstantExp(1))
                        )
                )
                //.Where(ComparisonExp(ConstantExp("STATUS"), SqlComparisonOperator.Equality, ConstantExp(1)))
                .Select("MAX(AMOUNT) AS max")
                .ToListAsync(r =>
                    //new Cl
                    //{
                    //    Status = (byte) r["max"],
                    //    Amount = (decimal) r[1],
                    //    RestrictCode = (string) r[2]
                    //}
                    r["max"]).Result;

            foreach (var l in list)
            {
                System.Console.WriteLine(l);
            }
        }
    }
}
