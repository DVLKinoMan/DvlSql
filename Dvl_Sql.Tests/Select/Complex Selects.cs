﻿using System;
using System.Text.RegularExpressions;
using DvlSql.Abstract;
using DvlSql.Expressions;
using NUnit.Framework;
using static DvlSql.ExpressionHelpers;
using static DvlSql.SqlType;
using DateTime = System.DateTime;

namespace DvlSql.Tests.Select
{
    [TestFixture]
    public class ComplexSelects
    {
        private readonly IDvlSql _sql1 =
            IDvlSql.DefaultDvlSql(
                "Data Source=SQL; Initial Catalog=BANK2000; Connection Timeout=30; User Id=b2000; Password=1234; Application Name = CoreApi");

        private readonly IDvlSql _sql2 =
            IDvlSql.DefaultDvlSql(
                @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        [Test]
        public void TestMethod1()
        {
            var select = this._sql1
                .From(AsExp("nbe.BANK_DATA", "B1"), true)
                .Join(AsExp("nbe.BANK_DATA", "B2"), ConstantExpCol("B1.REC_ID") == "B2.REC_ID")
                .Where(
                    ConstantExpCol("B1.AMOUNT") < 35000 &
                    !InExp("B1.REC_ID", SelectTopExp(FromExp("nbe.BANK_DATA"), 4, "REC_ID")) &
                    !!!LikeExp("B1.RESTRICT_CODE", "%dd%") &
                    ConstantExpCol("B1.ADD_DATE") > "@date"
                    , Params(Param("@date", new DateTime(2012, 1, 1)))
                )
                .GroupBy("B1.AMOUNT")
                .Having(ConstantExpCol("Count(*)") >= "2")
                .Select("B1.AMOUNT", AsExp(CountExp(), "[CountExp]"))
                .OrderByDescending("[CountExp]", "AMOUNT")
                .ToString();

            string expectedSelect = Regex.Escape(
                string.Format("SELECT B1.AMOUNT, COUNT(*) AS [CountExp] FROM nbe.BANK_DATA AS [B1] WITH(NOLOCK){0}" +
                              "INNER JOIN nbe.BANK_DATA AS [B2] ON B1.REC_ID = B2.REC_ID{0}" +
                              "WHERE ((( B1.AMOUNT < 35000 AND B1.REC_ID NOT IN ( SELECT TOP 4 REC_ID FROM nbe.BANK_DATA )) AND B1.RESTRICT_CODE NOT LIKE '%dd%' ) AND B1.ADD_DATE > @date ){0}" +
                              "GROUP BY B1.AMOUNT{0}" +
                              "HAVING Count(*) >= 2{0}" +
                              "ORDER BY [CountExp] DESC, AMOUNT DESC",
                    Environment.NewLine));

            Assert.That(Regex.Escape(select), Is.EqualTo(expectedSelect));
        }

        [Test]
        public void TestMethod2()
        {
            string actualSelect = this._sql2
                .From("dbo.Words")
                .Where(IsNullExp("Date"))
                .SelectTop(1)
                .OrderByDescending("Amount")
                .ToString();

            string expectedSelect = Regex.Escape(string.Format("SELECT TOP 1 * FROM dbo.Words{0}" +
                                                               "WHERE Date IS NULL{0}" +
                                                               "ORDER BY Amount DESC",
                Environment.NewLine));

            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }

        [Test]
        public void TestMethod3()
        {
            string actualSelect = this._sql2
                .From("dbo.Words")
                .Where(IsNullExp("Date"))
                .SelectTop(1, "Amount")
                .OrderByDescending("Amount")
                .ToString();

            string expectedSelect = Regex.Escape(string.Format("SELECT TOP 1 Amount FROM dbo.Words{0}" +
                                                               "WHERE Date IS NULL{0}" +
                                                               "ORDER BY Amount DESC",
                Environment.NewLine));

            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }

        [Test]
        public void TestMethod4()
        {
            var actualSelect = this._sql2
                .From("dbo.Words")
                .Where(!IsNullExp("Date"))
                .SelectTop(1, "Amount", "Date")
                .OrderByDescending("Amount")
                .ToString();

            string expectedSelect = Regex.Escape(string.Format("SELECT TOP 1 Amount, Date FROM dbo.Words{0}" +
                                                               "WHERE Date IS NOT NULL{0}" +
                                                               "ORDER BY Amount DESC",
                Environment.NewLine));

            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }

        [Test]
        public void TestMethod5()
        {
            var where = _sql1
                .From(AsExp("nbe.BANK_DATA", "B1"), true)
                .Join(AsExp("nbe.BANK_DATA", "B2"), ConstantExpCol("B1.REC_ID") == "B2.REC_ID")
                .Where(
                    ConstantExpCol("B1.AMOUNT") < 35000 &
                    !InExp("B1.REC_ID", SelectTopExp(FromExp("nbe.BANK_DATA"), 4, "REC_ID")) &
                    !!!LikeExp("B1.RESTRICT_CODE", "%dd%") &
                    ConstantExpCol("B1.ADD_DATE") > "@date"
                    , Params(Param("@date", new DateTime(2012, 1, 1)))
                );

            var actualSelect1 = where.Select()
                .ToString();

            var actualSelect2 = where.SelectTop(11)
                .OrderByDescending("B1.AMOUNT")
                .ToString();

            string expectedSelect1 = Regex.Escape(string.Format(
                "SELECT * FROM nbe.BANK_DATA AS [B1] WITH(NOLOCK){0}" +
                "INNER JOIN nbe.BANK_DATA AS [B2] ON B1.REC_ID = B2.REC_ID{0}" +
                "WHERE ((( B1.AMOUNT < 35000 AND B1.REC_ID NOT IN ( SELECT TOP 4 REC_ID FROM nbe.BANK_DATA )) AND B1.RESTRICT_CODE NOT LIKE '%dd%' ) AND B1.ADD_DATE > @date )",
                Environment.NewLine));

            string expectedSelect2 = Regex.Escape(string.Format(
                "SELECT TOP 11 * FROM nbe.BANK_DATA AS [B1] WITH(NOLOCK){0}" +
                "INNER JOIN nbe.BANK_DATA AS [B2] ON B1.REC_ID = B2.REC_ID{0}" +
                "WHERE ((( B1.AMOUNT < 35000 AND B1.REC_ID NOT IN ( SELECT TOP 4 REC_ID FROM nbe.BANK_DATA )) AND B1.RESTRICT_CODE NOT LIKE '%dd%' ) AND B1.ADD_DATE > @date ){0}" +
                "ORDER BY B1.AMOUNT DESC",
                Environment.NewLine));

            Assert.Multiple(
                () =>
                {
                    Assert.That(Regex.Escape(actualSelect1), Is.EqualTo(expectedSelect1));
                    Assert.That(Regex.Escape(actualSelect2), Is.EqualTo(expectedSelect2));
                });
        }

        private static object[][] _paramsForMethod = new[]
        {
            new object[] {true, new DateTime(2011, 11, 11), new DateTime(2019, 11, 11), 11},
            new object[] {false, new DateTime(2011, 11, 11), new DateTime(2019, 11, 11), 11}
        };

        [Test]
        [TestCaseSource(nameof(_paramsForMethod))]
        public void TestMethod6(bool onlyOwn, DateTime? startDateTime, DateTime? endDateTime, int userId)
        {
            var select = _sql1.From($"dbo.DocumentsArc X")
                .Where((IsNullExp("@startDate") | ConstantExpCol("X.CreatedAt") >= "@startDate") &
                       (IsNullExp("@endDate") | ConstantExpCol("X.CreatedAt") <= "@endDate") &
                       (onlyOwn
                           ? (DvlSqlBinaryExpression) (ConstantExpCol("X.WorkerUserId") == "@userId")
                           : ExistsExp(FullSelectExp(SelectExp(FromExp($"dbo.UserDocumentTypes UDT")),
                                 where: WhereExp(
                                     ConstantExpCol("UDT.Id") == "X.Type" & ConstantExpCol("UDT.UserId") == "@userId")))
                             | ConstantExpCol("X.WorkerUserId") == "@userId"
                       ),
                    Params(
                        Param("@startDate", DateTime(startDateTime ?? System.DateTime.Now)),
                        Param("@endDate", DateTime(endDateTime ?? System.DateTime.Now)),
                        Param("@userId", Int(userId))
                    ))
                .Select("X.*")
                .OrderBy("X.Priority")
                .OrderBy("X.CreatedAt")
                .ToString();
        }
    }
}