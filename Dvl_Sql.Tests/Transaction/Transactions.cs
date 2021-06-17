using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dvl_Sql.Abstract;
using NUnit.Framework;

using static Dvl_Sql.Helpers.Expressions;
using static Dvl_Sql.Helpers.SqlType;

namespace Dvl_Sql.Tests.Transaction
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
            await (await this._sql.BeginTransactionAsync(async () =>
            {
                var k = await this._sql.InsertInto<(int, string)>("dbo.Words", 
                        IntType("Id"), NVarCharType("Name", 50))
                    .Values((1, "Some New Word"), (2, "Some New Word 2"))
                    .ExecuteAsync();

                var d = await this._sql.Update("dbo.Words")
                    .Set(NVarChar("Name", "Updated Word", 50))
                    .Where(ConstantExp("Id") == 1)
                    .ExecuteAsync();

            })).CommitAsync();

        }
    }
}
