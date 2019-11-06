﻿using DVL_SQL_Test1.Abstract;
using DVL_SQL_Test1.Concrete;
using DVL_SQL_Test1.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static DVL_SQL_Test1.Helpers.DvlSqlExpressionHelpers;
using static DVL_SQL_Test1.Helpers.DvlSqlHelpers;
using static DVL_SQL_Test1.Models.CustomDvlSqlType;

namespace DVL_SQL_Test1.Tests
{
    [TestClass]
    public class DvlSqlDeleteTester
    {
        private readonly IDvlSql _sql =
            new DvlSql(
                @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        [TestMethod]
        public void TestMethod1()
        {
            var rows = _sql.DeleteFrom("dbo.Words")
                .Where(ComparisonExp(ConstantExp("Text"), SqlComparisonOperator.Equality, ConstantExp("@text")),
                    Params(Param("@text", NVarCharMax("New Text"))))
                .ExecuteAsync().Result;
        }

        [TestMethod]
        public void TestMethod2()
        {
            var rows = _sql.DeleteFrom("dbo.Words")
                .ExecuteAsync().Result;
        }
    }
}
