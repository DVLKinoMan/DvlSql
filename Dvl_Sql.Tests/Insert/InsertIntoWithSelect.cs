using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using DvlSql.Expressions;
using DvlSql.SqlServer;
using NUnit.Framework;

using static DvlSql.Extensions.ExpressionHelpers;
using static DvlSql.Extensions.SqlType;

namespace DvlSql.Tests.Insert
{
    [TestFixture]
    public class InsertIntoWithSelect
    {
         private readonly IDvlSql _sql =
            new DvlSqlMs(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        private static IEnumerable<string> Columns(params string[] cols) => cols;
        
        [Test]
        public void TestMethod1()
        {
            // ReSharper disable once UnusedVariable
            var actualInsert = this._sql
                .InsertInto("dbo.Words", Columns("Amount", "Text"))
                .SelectStatement(
                    FullSelectExp(
                        SelectTopExp(2, "Amount", "Text"),
                            FromExp("dbo.Words"),
                        orderBy: OrderByExp(
                            ("Text", Ordering.ASC)
                        )
                    )
                )
                .ToString();
            
            string expectedInsert = Regex.Escape(
                $"INSERT INTO dbo.Words ( Amount, Text ) SELECT TOP 2 Amount, Text FROM dbo.Words{Environment.NewLine}ORDER BY Text ASC");

            Assert.That(Regex.Escape(actualInsert), Is.EqualTo(expectedInsert));
        }

        [Test]
        public void TestMethod2()
        {
            // ReSharper disable once UnusedVariable
            var actualInsert = this._sql
                .InsertInto("dbo.Words", Columns("Amount", "Text"))
                .SelectStatement(FullSelectExp(SelectTopExp(2, "Amount", "Text"),FromExp("dbo.Words"),
                        orderBy: OrderByExp(("Text", Ordering.ASC)),
                        where: WhereExp(ConstantExpCol("Amount") == "@amount")),
                    Param("amount", Decimal(42))
                )
                .ToString();
            
            string expectedInsert = Regex.Escape(
                $"INSERT INTO dbo.Words ( Amount, Text ) SELECT TOP 2 Amount, Text FROM dbo.Words WHERE Amount = @amount{Environment.NewLine}ORDER BY Text ASC");

            Assert.That(Regex.Escape(actualInsert), Is.EqualTo(expectedInsert));
        }
    }
}