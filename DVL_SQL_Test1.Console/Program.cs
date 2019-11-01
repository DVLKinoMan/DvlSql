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
using static DVL_SQL_Test1.Helpers.DvlSqlAggregateFunctionHelpers;
using System.Linq;
using System.Runtime.CompilerServices;
using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Extensions;

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
            //ExecuteDvlSql(connString);//@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = CoreApi");
            ////watch.Stop();
            ////var seconds1 = watch.ElapsedMilliseconds;

            //ExecuteSqlR(
            //    @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = CoreApi");
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
            var k = (5, 3, 3);

            System.Console.WriteLine(k.GetType());

            IDvlSql sql = new DvlSql(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = CoreApi");
            var someres = sql
                //.InsertInto<(int, string)>("dbo.Words", Columns("Amount", "Text"))
                //.Values((42,"newVal1"), (43, "newVal2"), (44, "newVal3"))
                .InsertInto("dbo.Words", Columns("Amount", "Text"))
                .SelectStatement(SelectExp(FromExp("dbo.Words"), 2))
                .ExecuteAsync().Result;

            IEnumerable<string> Columns(params string[] cols)
            {
                foreach (var col in cols)
                    yield return col;
            }
        }

        public static void ExecuteDvlSql(string connString)
        {
            var list = new DvlSql(connString)
                .From(AsExp("nbe.BANK_DATA", "B1"), true)
                .Join(AsExp("nbe.BANK_DATA","B2"), ComparisonExp(ConstantExp("B1.REC_ID"), SqlComparisonOperator.Equality, ConstantExp("B2.REC_ID")))
                .Where(
                    AndExp(
                        ComparisonExp(ConstantExp("B1.AMOUNT"), SqlComparisonOperator.Less, ConstantExp(35000)),
                        NotInExp("B1.REC_ID", SelectTopExp(FromExp("nbe.BANK_DATA"), 4, "REC_ID")),
                        NotLikeExp("B1.RESTRICT_CODE","%dd%")
                        //ComparisonExp(ConstantExp("ADD_DATE"), SqlComparisonOperator.Less, ConstantExp(new DateTime(2012, 1, 1)))
                        //ComparisonExp(ConstantExp("B1.STATUS"), SqlComparisonOperator.Equality, ConstantExp(1))
                    )
                )
                .GroupBy("B1.AMOUNT")
                .Having(ComparisonExp(ConstantExp("Count(*)"), SqlComparisonOperator.GreaterOrEqual, ConstantExp("2")))
                //.Where(ComparisonExp(ConstantExp("STATUS"), SqlComparisonOperator.Equality, ConstantExp(1)))
                //.SelectTop(4,"B1.STATUS", "B1.AMOUNT", "B1.RESTRICT_CODE")
                //.Select("B1.STATUS", "B1.AMOUNT", "B1.RESTRICT_CODE")
                .Select("B1.AMOUNT", AsExp(CountExp(), "[CountExp]"))
                //.OrderBy("B1.AMOUNT")
                .OrderByDescending("[CountExp]", "AMOUNT")
                //.OrderBy()
                .ToListAsync(r =>
                        new 
                        {
                            //Status = (byte)r["STATUS"],
                            Amount = (decimal)r["AMOUNT"],
                            //RestrictCode = (string)r["RESTRICT_CODE"]
                            Count = (int)r["CountExp"]
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
