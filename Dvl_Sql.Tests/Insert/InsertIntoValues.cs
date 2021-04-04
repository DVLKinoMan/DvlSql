using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Dvl_Sql.Abstract;
using NUnit.Framework;

using static Dvl_Sql.Helpers.SqlType;

namespace Dvl_Sql.Tests.Insert
{
    [TestFixture]
    public class InsertIntoValues
    {
         private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        private static IEnumerable<string> Columns(params string[] cols) => cols;

        [Test]
        public void InsertIntoWith3Columns()
        {
            // ReSharper disable once UnusedVariable
            var actualInsert = this._sql
                .InsertInto<int, string>("dbo.Words",
                    DecimalType("Amount"),
                    NVarCharType("Text", 100))
                .Values(
                    (43, "newVal2"),
                    (44, "newVal3"),
                    (42, "newVal1")
                )
                .ToString();
            
            string expectedInsert = Regex.Escape(
                string.Format("INSERT INTO dbo.Words ( Amount, Text ){0}" +
                              "VALUES{0}" +
                              "( @Amount1, @Text1 ),{0}" +
                              "( @Amount2, @Text2 ),{0}" +
                              "( @Amount3, @Text3 )", 
                    Environment.NewLine));

            Assert.That(Regex.Escape(actualInsert), Is.EqualTo(expectedInsert));
        }
        
        [Test]
        public void InsertIntoWith6Columns()
        {
            var actualInsert = _sql.InsertInto<int, string, string, string, int, int>(
                    "[ManagerConfirmation].[confirmation].[Users]",
                    IntType("userId"),
                    NVarCharType("firstName", 100),
                    NVarCharType("lastName", 100),
                    NVarCharType("mobileNumber", 20),
                    TinyIntType("status"),
                    NVarCharType("deviceId", 100))
                .Values(
                    (2, "ass", "ass", "ass", 1, 1)
                )
                .ToString();

            string expectedInsert = Regex.Escape(
                string.Format(
                    "INSERT INTO [ManagerConfirmation].[confirmation].[Users] ( userId, firstName, lastName, mobileNumber, status, deviceId ){0}" +
                    "VALUES{0}" +
                    "( @userId1, @firstName1, @lastName1, @mobileNumber1, @status1, @deviceId1 )",
                    Environment.NewLine));

            Assert.That(Regex.Escape(actualInsert), Is.EqualTo(expectedInsert));
        }
    }
}