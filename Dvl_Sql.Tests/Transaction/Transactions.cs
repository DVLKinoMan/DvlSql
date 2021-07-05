using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DvlSql.Abstract;
using DvlSql.Tests.Result;
using NUnit.Framework;

using static DvlSql.ExpressionHelpers;
using static DvlSql.SqlType;
using static DvlSql.DataReader;

namespace DvlSql.Tests.Transaction
{
    [TestFixture]
    class Transactions
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=DVL_Test; Connection Timeout=30; Application Name = DVLSqlTest1");

        //todo normal test
        [Test]
        public async Task TestMethod1()
        {
            var conn = await this._sql.BeginTransactionAsync();
            await this._sql.SetConnection(conn).DeleteFrom("dbo.Words")
                .ExecuteAsync();

            var k = await this._sql.SetConnection(conn).InsertInto<(int, string)>("dbo.Words",
                    IntType("Id"), NVarCharType("Name", 50))
                .Values((1, "Some New Word"), (2, "Some New Word 2"))
                .ExecuteAsync();

            var d = await this._sql.SetConnection(conn).Update("dbo.Words")
                .Set(NVarChar("Name", "Updated Word", 50))
                .Where(ConstantExpCol("Id") == 1)
                .ExecuteAsync();

            await this._sql.SetConnection(conn).CommitAsync();
        }


        //todo normal test
        [Test]
        public async Task TestMethod2()
        {
            var table = this._sql.DeclareTable("inserted")
                .AddColumns(IntType("id", true));

            var conn = await this._sql.BeginTransactionAsync();
            await this._sql.SetConnection(conn).DeleteFrom("dbo.Words")
                .ExecuteAsync();

            var k = await this._sql.SetConnection(conn).InsertInto<(int, string)>("dbo.Words",
                    IntType("Id"), NVarCharType("Name", 50))
                .Output(AsList (r=>int.Parse(r["id"].ToString())),"inserted.id")
                .Values((1, "Some New Word"), (2, "Some New Word 2"))
                .ExecuteAsync();

            await this._sql.SetConnection(conn).CommitAsync();
        }
    }
}
