using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Concrete;
using DVL_SQL_Test1.Expressions;
using DVL_SQL_Test1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;

using static DVL_SQL_Test1.Helpers.DvlSqlExpressionHelpers;

namespace DVL_SQL_Test1.Tests
{
    [TestClass]
    public class DvlSqlInsertTester
    {
        private readonly IDvlSql _sql =
            new DvlSql(
                @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        private static IEnumerable<string> Columns(params string[] cols) => cols;

        [TestMethod]
        public void TestMethod1()
        {
            var affectedRows = _sql
                .InsertInto("dbo.Words", Columns("Amount", "Text"))
                .SelectStatement(FullSelectExp(SelectTopExp(FromExp("dbo.Words"), 2, "Amount", "Text"),
                    orderByExpression: OrderByExp(("Text", Ordering.ASC))))
                .ExecuteAsync().Result;

            //Assert.AreEqual(affectedRows, 2);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var affectedRows = _sql
                .InsertInto<(int, string)>("dbo.Words", ("Amount", new DvlSqlType(SqlDbType.Decimal)), ("Text", new DvlSqlType(SqlDbType.NVarChar)))
                .Values((42, "newVal1"), (43, "newVal2"), (44, "newVal3"))
                .ExecuteAsync().Result;

            //Assert.AreEqual(affectedRows, 3);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var affectedRows = _sql
                .InsertInto("dbo.Words", Columns("Amount", "Text"))
                .SelectStatement(FullSelectExp(SelectTopExp(FromExp("dbo.Words"), 2, "Amount", "Text"),
                        orderByExpression: OrderByExp(("Text", Ordering.ASC)),
                        sqlWhereExpression: WhereExp(ComparisonExp(ConstantExp("Amount"),
                            SqlComparisonOperator.Equality, ConstantExp("@amount")))),
                    new DvlSqlParameter<int>("amount", new DvlSqlType<int>("@amount", 42, SqlDbType.Decimal))
                )
                .ExecuteAsync().Result;

            //Assert.AreEqual(affectedRows, 2);
        }
    }
}
