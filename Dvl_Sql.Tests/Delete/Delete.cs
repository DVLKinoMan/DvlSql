﻿using DvlSql.SqlServer;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;
using static DvlSql.ExpressionHelpers;
using static DvlSql.SqlType;

namespace DvlSql.Tests.Delete
{
    [TestFixture]
    public class Delete
    {
        private readonly IDvlSql _sql =
            new DvlSqlMs(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        [Test]
        public void TestMethod1()
        {
            var actualDelete = this._sql.DeleteFrom("dbo.Words")
                .Where(ConstantExpCol("Text") == "@text",
                    Params(
                        Param("@text", NVarCharMax("New Text"))
                    ))
                .ToString();
            
            string expectedDelete = Regex.Escape(
                $"DELETE FROM dbo.Words{Environment.NewLine}" +
                $"WHERE Text = @text");

            Assert.That(Regex.Escape(actualDelete), Is.EqualTo(expectedDelete));
        }

        [Test]
        public void TestMethod2()
        {
            var actualDelete = this._sql.DeleteFrom("dbo.Words")
                .ToString();
            
            string expectedDelete = Regex.Escape(
                @"DELETE FROM dbo.Words");

            Assert.That(Regex.Escape(actualDelete), Is.EqualTo(expectedDelete));
        }

        [Test]
        public void TestMethod3()
        {
            var actualDelete = this._sql.DeleteFrom("dbo.Words")
                .Output(r=>r["deleted.id"], "deleted.Id")
                .Where(ConstantExp("Id", true)==2)
                .ToString();

            string expectedDelete = Regex.Escape(
                $@"DELETE FROM dbo.Words{Environment.NewLine}OUTPUT deleted.Id{Environment.NewLine}WHERE Id = 2");

            Assert.That(Regex.Escape(actualDelete), Is.EqualTo(expectedDelete));
        }

        [Test]
        public void TestMethod4()
        {
            var actualDelete = this._sql.DeleteFrom(FromExp("Words", "w"))
                .Join("Words", "w.Id", "Words.Id")
                .Where(ConstantExp("Id", true) == 2)
                .ToString();

            string expectedDelete = Regex.Escape(
                $@"DELETE w FROM Words AS w{Environment.NewLine}INNER JOIN Words ON w.Id = Words.Id{Environment.NewLine}WHERE Id = 2");

            Assert.That(Regex.Escape(actualDelete), Is.EqualTo(expectedDelete));
        }
    }
}