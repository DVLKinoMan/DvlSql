using System;
using System.Collections.Generic;
using System.Data;
using Dvl_Sql.Abstract;
using NUnit.Framework;

using static Dvl_Sql.Tests.Result.Helpers;

namespace Dvl_Sql.Tests.Result
{
    [TestFixture]
    public class ToDictionary
    {
         private string TableName = "dbo.Words";

        #region Parameters

        private static readonly object[] ParametersWithFunc =
            new[]
            {
                new object[]
                {
                    (Func<IDataReader, int>) (r => (int) r[0]),
                    (Func<IDataReader, int>) (r => (int) r[0] + 1),
                    new List<int>() {1, 1, 2, 2, 3}, 
                    new Dictionary<int, List<int>>()
                    {
                        {1, new List<int>{2,2}},
                        {2, new List<int>{3,3}},
                        {3, new List<int>{4}}
                    }
                },
                // new object[]
                // {
                //     (Func<IDataReader, string>) (r => ((string) r[0]).Substring(0, 1)),
                //     (Func<IDataReader, string>) (r => ((string) r[0]).Substring(0, 1)),
                //     new List<string>() {"David", "Lasha", "SomeGuy"}, new List<string>() {"D", "L", "S"}
                // },
                // new object[]
                // {
                //     (Func<IDataReader, SomeClass>) (r =>
                //     {
                //         var someClass = (SomeClass) r[0];
                //         return new SomeClass(someClass.SomeIntField + 1,
                //             someClass.SomeStringField.Substring(0, 1));
                //     }),
                //     (Func<IDataReader, SomeClass>) (r =>
                //     {
                //         var someClass = (SomeClass) r[0];
                //         return new SomeClass(someClass.SomeIntField + 1,
                //             someClass.SomeStringField.Substring(0, 1));
                //     }),
                //     new List<SomeClass>()
                //         {new SomeClass(1, "David"), new SomeClass(2, "Lasha"), new SomeClass(3, "SomeGuy")},
                //     new List<SomeClass>()
                //         {new SomeClass(2, "D"), new SomeClass(3, "L"), new SomeClass(4, "S")}
                // }
            };

        #endregion

        //todo: Not working
        [Test]
        [TestCaseSource(nameof(ParametersWithFunc))]
        public void TestToDictionary<T>(Func<IDataReader, T> keySelector, Func<IDataReader, T> valueSelector, List<T> data, Dictionary<T,List<T>> expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<Dictionary<T,List<T>>>(readerMoq);
            var moq = CreateConnectionMock<Dictionary<T,List<T>>>(commandMoq);

            var dictionary = IDvlSql.DefaultDvlSql(moq.Object)
                .From(TableName)
                .Select()
                .ToDictionaryAsync(keySelector, valueSelector)
                .Result;

            Assert.That(dictionary, Is.EquivalentTo(expected));
        }
        
    }
}