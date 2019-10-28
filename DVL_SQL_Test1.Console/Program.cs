using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DVL_SQL_Test1.Concrete;
using DVL_SQL_Test1.Expressions;
using static DVL_SQL_Test1.Helpers.DvlSqlExpressionHelpers;
using System.Diagnostics;
using SqlR;
using static SqlR.Functions;

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

        public class Cl2
        {
            public decimal Amount;
            public decimal Amount2;
            public int REC1;
            public int REC2;
        }

        static void Main()
        {
            string connString =
                "Data Source=SQL; Initial Catalog=BANK2000; Connection Timeout=30; User Id=b2000; Password=1234; Application Name = CoreApi";

            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            var list = ExecuteDvlSql(connString);
            //watch.Stop();
            //var seconds1 = watch.ElapsedMilliseconds;


            //watch.Reset();
            //watch.Start();
            //ExecuteSqlR(connString);
            //watch.Stop();
            //var seconds2 = watch.ElapsedMilliseconds;

            foreach (var l in list)
            {
                System.Console.WriteLine(l);
            }

            //System.Console.WriteLine($"My Execution time: {seconds1}; SqlR Execution time: {seconds2}");
        }

        public static List<Cl> ExecuteDvlSql(string connString)
        {
            var list = new DvlSql(connString)
                .From("nbe.BANK_DATA", true)
                .Where(
                    AndExp(
                        ComparisonExp(ConstantExp("AMOUNT"), SqlComparisonOperator.Less, ConstantExp(350000)),
                        //ComparisonExp(ConstantExp("ADD_DATE"), SqlComparisonOperator.Less, ConstantExp(new DateTime(2012, 1, 1)))
                        ComparisonExp(ConstantExp("STATUS"), SqlComparisonOperator.Equality, ConstantExp(1))
                    )
                )
                .OrderBy("AMOUNT")
                .OrderBy("RESTRICT_CODE")
                //.Where(ComparisonExp(ConstantExp("STATUS"), SqlComparisonOperator.Equality, ConstantExp(1)))
                .Select("STATUS","AMOUNT","RESTRICT_CODE")
                .ToListAsync(r =>
                        new Cl
                        {
                            Status = (byte) r["STATUS"],
                            Amount = (decimal) r["AMOUNT"],
                            RestrictCode = (string) r["RESTRICT_CODE"]
                        }
                ).Result;

            return list;
        }
        
        public static void ExecuteSqlR(string connString)
        {
            var sqlr = SqlR.SqlR.Sql(connString);
            var resList = sqlr.Query(
                    @"SELECT s.AMOUNT AS AM1, s.REC_ID AS REC1, s1.REC_ID AS REC2, s1.AMOUNT AS AM2 FROM nbe.BANK_DATA s1 WITH(NOLOCK) INNER JOIN  nbe.BANK_DATA s ON s.REC_ID <> s1.REC_ID INNER JOIN  nbe.BANK_DATA s3 ON s.REC_ID <> s3.REC_ID WHERE s.AMOUNT < 350000  AND s1.STATUS = 1 ",
                    new List<SqlRParameter>(),
                    AsList(r => new Cl2
                    {
                        Amount = r["AM1"] is DBNull ? 0 : (decimal) r["AM1"],
                        Amount2 = r["AM2"] is DBNull ? 0 : (decimal) r["AM2"],
                        REC1 = (int) r["REC1"],
                        REC2 = (int) r["REC2"]
                    }))
                .ExecuteAsync().Result;
        }
    }
}
