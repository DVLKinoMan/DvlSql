﻿using Dvl_Sql.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Dvl_Sql.Extensions.Expressions.ExpressionHelpers;
using static Dvl_Sql.Extensions.Types.TypeHelpers;

namespace Dvl_Sql.Tests
{
    [TestClass]
    public class DvlSqlDeleteTester
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        [TestMethod]
        public void TestMethod1()
        {
            var rows = this._sql.DeleteFrom("dbo.Words")
                .Where(ConstantExp("Text") == ConstantExp("@text"),
                    Params(
                        Param("@text", NVarCharMax("New Text"))
                    ))
                .ExecuteAsync().Result;
        }

        [TestMethod]
        public void TestMethod2()
        {
            var rows = this._sql.DeleteFrom("dbo.Words")
                .ExecuteAsync().Result;
        }
    }
}
