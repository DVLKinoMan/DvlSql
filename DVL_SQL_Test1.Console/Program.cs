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
            //public byte Status;
            //public decimal Amount;
            //public string RestrictCode;
            public decimal Amount;
            public decimal Amount2;
            public int REC1;
            public int REC2;

            //public override string ToString() => $"{this.Status} {this.Amount} {this.RestrictCode}";
        }

        static void Main()
        {
            while (true)
            {
                string connString =
                    "Data Source=SQL; Initial Catalog=BANK2000; Connection Timeout=30; User Id=b2000; Password=1234; Application Name = CoreApi";

                Stopwatch watch = new Stopwatch();
                watch.Start();
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
                        r["max"]
                    //new Cl
                    //{
                    //    Amount = r["AM1"] is DBNull ? 0 : (decimal) r["AM1"],
                    //    Amount2 = r["AM2"] is DBNull ? 0 : (decimal) r["AM2"],
                    //    REC1 = (int) r["REC1"],
                    //    REC2 = (int) r["REC2"]
                    //}
                    ).Result;
                watch.Stop();
                var seconds1 = watch.ElapsedMilliseconds;

                //var sqlr = SqlR.SqlR.Sql(connString);
                //watch.Reset();
                //watch.Start();
                //var resList = sqlr.Query<List<Cl>>(
                //        @"SELECT s.AMOUNT AS AM1, s.REC_ID AS REC1, s1.REC_ID AS REC2, s1.AMOUNT AS AM2 FROM nbe.BANK_DATA s1 WITH(NOLOCK) INNER JOIN  nbe.BANK_DATA s ON s.REC_ID <> s1.REC_ID INNER JOIN  nbe.BANK_DATA s3 ON s.REC_ID <> s3.REC_ID WHERE s.AMOUNT < 350000  AND s1.STATUS = 1 ",
                //        new List<SqlRParameter>(), 
                //        AsList<Cl>(r => new Cl
                //        {
                //            Amount = r["AM1"] is DBNull ? 0 : (decimal) r["AM1"],
                //            Amount2 = r["AM2"] is DBNull ? 0 : (decimal) r["AM2"],
                //            REC1 = (int) r["REC1"],
                //            REC2 = (int) r["REC2"]
                //        }))
                //    .ExecuteAsync().Result;
                //watch.Stop();
                //var seconds2 = watch.ElapsedMilliseconds;

                //System.Console.WriteLine($"My Execution time: {seconds1}; SqlR Execution time: {seconds2}");
            }

            //foreach (var l in list)
            //{
            //    System.Console.WriteLine(l);
            //}
        }
    }
}
