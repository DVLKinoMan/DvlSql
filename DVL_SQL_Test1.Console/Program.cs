using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DVL_SQL_Test1.Concrete;
using DVL_SQL_Test1.Expressions;
using static DVL_SQL_Test1.Helpers.DvlSqlExpressionHelpers;
using System.Diagnostics;
using SqlR;
//using static SqlR.Functions;
using static DVL_SQL_Test1.Helpers.DvlSqlAggregateFunctionHelpers;
using static DVL_SQL_Test1.Helpers.DvlSqlHelpers;
using static DVL_SQL_Test1.Models.CustomDvlSqlType;
using System.Linq;
using System.Runtime.CompilerServices;
using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Extensions;
using DVL_SQL_Test1.Models;

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

            IDvlSql sql = new DvlSql(connString);

            IEnumerable<string> Columns(params string[] cols)
            {
                foreach (var col in cols)
                    yield return col;
            }
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
            //var resList = sqlr.Query(
            //        @"SELECT * FROM dbo.Words",
            //        new List<SqlRParameter>(),
            //        AsList(r => new
            //        {
            //            REC1 = (int)r["WordId"],
            //            REC2 = (string)r["Text"]
            //        })
            //        , SqlRQueryHint.SingleResult)
            //    .ExecuteAsync().Result;
        }
    }

}
