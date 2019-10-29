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
            ExecuteDvlSql(connString);//@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = CoreApi");
            //watch.Stop();
            //var seconds1 = watch.ElapsedMilliseconds;

            ExecuteSqlR(
                @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = CoreApi");
            //watch.Reset();
            //watch.Start();
            //ExecuteSqlR(connString);
            //watch.Stop();
            //var seconds2 = watch.ElapsedMilliseconds;

            //foreach (var l in list)
            //{
            //    System.Console.WriteLine(l);
            //}

            //System.Console.WriteLine($"My Execution time: {seconds1}; SqlR Execution time: {seconds2}");
        }

        public static void ExecuteDvlSql(string connString)
        {
            var list = new DvlSql(connString)
                .From("nbe.BANK_DATA AS B1", true)
                .Join("nbe.BANK_DATA AS B2", ComparisonExp(ConstantExp("B1.REC_ID"), SqlComparisonOperator.Different, ConstantExp("B2.REC_ID")))
                .Where(
                    AndExp(
                        ComparisonExp(ConstantExp("B1.AMOUNT"), SqlComparisonOperator.Equality, ConstantExp(500))
                        //ComparisonExp(ConstantExp("ADD_DATE"), SqlComparisonOperator.Less, ConstantExp(new DateTime(2012, 1, 1)))
                        //ComparisonExp(ConstantExp("B1.STATUS"), SqlComparisonOperator.Equality, ConstantExp(1))
                    )
                )
                .OrderBy("B1.AMOUNT")
                .OrderByDescending("B2.RESTRICT_CODE")
                //.Where(ComparisonExp(ConstantExp("STATUS"), SqlComparisonOperator.Equality, ConstantExp(1)))
                .SelectTop(4,"B1.STATUS", "B1.AMOUNT", "B1.RESTRICT_CODE")
                .ToListAsync(r =>
                        new Cl
                        {
                            Status = (byte)r["STATUS"],
                            Amount = (decimal)r["AMOUNT"],
                            RestrictCode = (string)r["RESTRICT_CODE"]
                        }
                ).Result;

            //var list = new DvlSql(connString)
            //    .From("dbo.Words", true)
            //    .Where(AndExp())
            //    .OrderByDescending("Text")
            //    .OrderBy("")
            //    .Select()
            //    .ToListAsync(r =>
            //        new
            //        {
            //            WordId = (int)r["WordId"],
            //            Text = (string)r["Text"]
            //        }
            //    ).Result;

            //return list;
        }
        
        public static void ExecuteSqlR(string connString)
        {
            //var sqlr = SqlR.SqlR.Sql(connString);
            //var resList = sqlr.Query(
            //        @"SELECT s.AMOUNT AS AM1, s.REC_ID AS REC1, s1.REC_ID AS REC2, s1.AMOUNT AS AM2 FROM nbe.BANK_DATA s1 WITH(NOLOCK) INNER JOIN  nbe.BANK_DATA s ON s.REC_ID <> s1.REC_ID INNER JOIN  nbe.BANK_DATA s3 ON s.REC_ID <> s3.REC_ID WHERE s.AMOUNT < 350000  AND s1.STATUS = 1 ",
            //        new List<SqlRParameter>(),
            //        AsList(r => new Cl2
            //        {
            //            Amount = r["AM1"] is DBNull ? 0 : (decimal) r["AM1"],
            //            Amount2 = r["AM2"] is DBNull ? 0 : (decimal) r["AM2"],
            //            REC1 = (int) r["REC1"],
            //            REC2 = (int) r["REC2"]
            //        })
            //        , SqlRQueryHint.SingleRecord)
            //    .ExecuteAsync().Result;
            var sqlr = SqlR.SqlR.Sql(connString);
            var resList = sqlr.Query(
                    @"SELECT * FROM dbo.Words",
                    new List<SqlRParameter>(),
                    AsList(r => new
                    {
                        REC1 = (int)r["WordId"],
                        REC2 = (string)r["Text"]
                    })
                    , SqlRQueryHint.SingleResult)
                .ExecuteAsync().Result;
        }
    }
}
