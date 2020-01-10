using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;
using Dvl_Sql.Abstract;
using Dvl_Sql.Expressions;
using Dvl_Sql.Models;
using static Dvl_Sql.Helpers.DvlSqlExpressionHelpers;

namespace Dvl_Sql.Tests
{
    [TestClass]
    public class DvlSqlInsertTester
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        private static IEnumerable<string> Columns(params string[] cols) => cols;

        [TestMethod]
        public void TestMethod1()
        {
            // ReSharper disable once UnusedVariable
            var affectedRows = this._sql
                .InsertInto("dbo.Words", Columns("Amount", "Text"))
                .SelectStatement(
                    FullSelectExp(
                        SelectTopExp(
                            FromExp("dbo.Words"), 2, "Amount", "Text"),
                        orderByExpression: OrderByExp(
                            ("Text", Ordering.ASC)
                        )
                    )
                )
                .ExecuteAsync().Result;

            //Assert.AreEqual(affectedRows, 2);
        }

        [TestMethod]
        public void TestMethod2()
        {
            // ReSharper disable once UnusedVariable
            var affectedRows = this._sql
                .InsertInto<(int, string)>("dbo.Words", ("Amount", new DvlSqlType(SqlDbType.Decimal)),
                    ("Text", new DvlSqlType(SqlDbType.NVarChar)))
                .Values(
                    (42, "newVal1"),
                    (43, "newVal2"),
                    (44, "newVal3")
                )
                .ExecuteAsync().Result;

            //Assert.AreEqual(affectedRows, 3);
        }

        [TestMethod]
        public void TestMethod3()
        {
            // ReSharper disable once UnusedVariable
            var affectedRows = this._sql
                .InsertInto("dbo.Words", Columns("Amount", "Text"))
                .SelectStatement(FullSelectExp(SelectTopExp(FromExp("dbo.Words"), 2, "Amount", "Text"),
                        orderByExpression: OrderByExp(("Text", Ordering.ASC)),
                        sqlWhereExpression: WhereExp(ConstantExp("Amount") == ConstantExp("@amount"))),
                    new DvlSqlParameter<int>("amount", new DvlSqlType<int>("@amount", 42, SqlDbType.Decimal))
                )
                .ExecuteAsync().Result;

            //Assert.AreEqual(affectedRows, 2);
        }
    }
}
