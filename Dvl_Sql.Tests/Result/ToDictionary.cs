using System;
using System.Collections.Generic;
using System.Data;
using DvlSql.Abstract;
using DvlSql.Tests.Classes;
using NUnit.Framework;
using static DvlSql.Tests.Result.Helpers;

namespace DvlSql.Tests.Result
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
                        {1, new List<int> {2, 2}},
                        {2, new List<int> {3, 3}},
                        {3, new List<int> {4}}
                    }
                },
                new object[]
                {
                    (Func<IDataReader, string>) (r => ((string) r[0]).Substring(0, 1)),
                    (Func<IDataReader, int>) (r => ((string) r[0]).Length),
                    new List<string>() {"david", "box", "david", "baby", "boy", "barbi", "cable"},
                    new Dictionary<string, List<int>>()
                    {
                        {"d", new List<int>() {5, 5}},
                        {"b", new List<int>() {3, 4, 3, 5}},
                        {"c", new List<int>() {5}},
                    },
                },
                new object[]
                {
                    (Func<IDataReader, int>) (r => ((string) r[0]).Length),
                    (Func<IDataReader, string>) (r => "Name: " + (string) r[0]),
                    new List<string>() {"david", "box", "david", "baby", "boy", "barbi", "cable"},
                    new Dictionary<int, List<string>>()
                    {
                        {5, new List<string>() {"Name: david", "Name: david", "Name: barbi", "Name: cable"}},
                        {3, new List<string>() {"Name: box", "Name: boy"}},
                        {4, new List<string>() {"Name: baby"}},
                    },
                },
                new object[]
                {
                    (Func<IDataReader, int>) (r =>
                    {
                        var someClass = (SomeClass) r[0];
                        return someClass.SomeIntField +
                               someClass.SomeStringField.Length;
                    }),
                    (Func<IDataReader, SomeClass>) (r =>
                    {
                        var someClass = (SomeClass) r[0];
                        return new SomeClass(someClass.SomeIntField,
                            someClass.SomeStringField.Substring(0, 1));
                    }),
                    new List<SomeClass>()
                        {new SomeClass(1, "David"), new SomeClass(2, "Lasha"), new SomeClass(-1, "SomeGuy")},
                    new Dictionary<int, List<SomeClass>>()
                    {
                        {6, new List<SomeClass> {new SomeClass(1, "D"), new SomeClass(-1, "S")}},
                        {7, new List<SomeClass> {new SomeClass(2, "L")}}
                    }
                }
            };

        #endregion

        [Test]
        [TestCaseSource(nameof(ParametersWithFunc))]
        public void TestToDictionary<TKey, TValue, TData>(Func<IDataReader, TKey> keySelector,
            Func<IDataReader, TValue> valueSelector, List<TData> data, Dictionary<TKey, List<TValue>> expected)
        {
            var readerMoq = CreateDataReaderMock(data);
            var commandMoq = CreateSqlCommandMock<Dictionary<TKey, List<TValue>>>(readerMoq);
            var moq = CreateConnectionMock<Dictionary<TKey, List<TValue>>>(commandMoq);

            var dictionary = IDvlSql.DefaultDvlSql(moq.Object)
                .From(TableName)
                .Select()
                .ToDictionaryAsync(keySelector, valueSelector)
                .Result;

            Assert.That(dictionary, Is.EquivalentTo(expected));
        }
    }
}