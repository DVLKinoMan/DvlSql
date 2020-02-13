using System;
using System.Text.RegularExpressions;
using Dvl_Sql.Abstract;
using NUnit.Framework;
using static Dvl_Sql.Extensions.Expressions;

namespace Dvl_Sql.Tests.Select
{
    [TestFixture]
    public class Where
    {
        private readonly IDvlSql _sql =
            IDvlSql.DefaultDvlSql(
                "test");

        private string TableName = "dbo.Words";

        [Test]
        [TestCase("Chars", 1)]
        public void WhereWithIntConstantExpression(string colName, int value)
        {
            var actualSelect = this._sql.From(TableName)
                .Where(ConstantExp(colName) == ConstantExp(value))
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape(
                $"SELECT * FROM {TableName}{Environment.NewLine}" + 
                $"WHERE {colName} = {value}"
            );
            
            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
        
        [Test]
        [TestCase("Text","David")]
        public void WhereWithStringConstantExpression(string colName, string value)
        {
            var actualSelect = this._sql.From(TableName)
                .Where(ConstantExp(colName) == ConstantExp(value))
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape(
                $"SELECT * FROM {TableName}{Environment.NewLine}" + 
                $"WHERE {colName} = {value}"
            );
            
            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
        
        [Test]
        [TestCase("Amount", 5.25)]
        public void WhereWithDoubleConstantExpression(string colName, double value)
        {
            var actualSelect = this._sql.From(TableName)
                .Where(ConstantExp(colName) == ConstantExp(value))
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape(
                $"SELECT * FROM {TableName}{Environment.NewLine}" + 
                $"WHERE {colName} = {value}"
            );
            
            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
        
        [Test]
        public void WhereWithDateTimeConstantExpression()
        {
            var dateTime = new DateTime(2019, 11, 11);
            var actualSelect = this._sql.From(TableName)
                .Where(ConstantExp("CreatedTime") == ConstantExp(dateTime))
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape(
                $"SELECT * FROM {TableName}{Environment.NewLine}" + 
                $"WHERE CreatedTime = {dateTime.ToString()}"
            );
            
            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
        
        [Test]
        [TestCase("Amount", 5.25)]
        [TestCase("Number", 2.9)]
        public void WhereWithGreaterExpression(string colName, double value)
        {
            var actualSelect = this._sql.From(TableName)
                .Where(ConstantExp(colName) > ConstantExp(value))
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape(
                $"SELECT * FROM {TableName}{Environment.NewLine}" + 
                $"WHERE {colName} > {value}"
            );
            
            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
        
        [Test]
        [TestCase("text")]
        public void WhereWithIsNullExpression(string fieldName)
        {
            var actualSelect = this._sql.From(TableName)
                .Where(IsNullExp(ConstantExp(fieldName)))
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape(
                $"SELECT * FROM {TableName}{Environment.NewLine}" + 
                $"WHERE {fieldName} IS NULL"
            );
            
            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
        
        [Test]
        [TestCase("text")]
        public void WhereWithIsNotNullExpression(string fieldName)
        {
            var actualSelect1 = this._sql.From(TableName)
                .Where(IsNotNullExp(ConstantExp(fieldName)))
                .Select()
                .ToString();
            
            var actualSelect2 = this._sql.From(TableName)
                .Where(!IsNullExp(ConstantExp(fieldName)))
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape(
                $"SELECT * FROM {TableName}{Environment.NewLine}" + 
                $"WHERE {fieldName} IS NOT NULL"
            );
            Assert.Multiple(
                () =>
                {
                    Assert.That(Regex.Escape(actualSelect1), Is.EqualTo(expectedSelect));
                    Assert.That(Regex.Escape(actualSelect2), Is.EqualTo(expectedSelect));
                });
        }
        
        [Test]
        [TestCase("Name","%d")]
        public void WhereWithLikeExpression(string fieldName, string pattern)
        {
            var actualSelect = this._sql.From(TableName)
                .Where(LikeExp(fieldName, pattern))
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape(
                $"SELECT * FROM {TableName}{Environment.NewLine}" + 
                $"WHERE {fieldName} LIKE '{pattern}'"
            );
            
            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
        
        [Test]
        [TestCase("Name","%d")]
        public void WhereWithNotLikeExpression(string fieldName, string pattern)
        {
            var actualSelect1 = this._sql.From(TableName)
                .Where(NotLikeExp(fieldName, pattern))
                .Select()
                .ToString();
            
            var actualSelect2 = this._sql.From(TableName)
                .Where(!LikeExp(fieldName, pattern))
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape(
                $"SELECT * FROM {TableName}{Environment.NewLine}" + 
                $"WHERE {fieldName} NOT LIKE '{pattern}'"
            );
            
            Assert.Multiple(
                () =>
                {
                    Assert.That(Regex.Escape(actualSelect1), Is.EqualTo(expectedSelect));
                    Assert.That(Regex.Escape(actualSelect2), Is.EqualTo(expectedSelect));
                });
        }
        
        [Test]
        [TestCase("Id")]
        public void WhereWithInExpression(string col)
        {
            var actualSelect = this._sql.From(TableName)
                .Where(InExp(col, ConstantExp(1), ConstantExp(2), ConstantExp(3)))
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape(
                $"SELECT * FROM {TableName}{Environment.NewLine}" + 
                $"WHERE {col} IN ( 1, 2, 3 )"
            );
            
            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
        
        [Test]
        [TestCase("Id", "dbo.Sentences")]
        public void WhereWithInSelectExpression(string col, string tableName)
        {
            var actualSelect = this._sql.From(TableName)
                .Where(InExp(col, SelectExp(FromExp(tableName), col)))
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape(
                $"SELECT * FROM {TableName}{Environment.NewLine}" + 
                $"WHERE {col} IN ( SELECT {col} FROM {tableName} )"
            );
            
            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }

        [Test]
        [TestCase("Amount", "Chars")]
        public void AndWithConstantExpressions(string col1, string col2)
        {
            var actualSelect = this._sql.From(TableName)
                .Where(ConstantExp(col1) == ConstantExp(5.25) &
                       ConstantExp(col2) == ConstantExp(3))
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape(
                $"SELECT * FROM {TableName}{Environment.NewLine}" +
                $"WHERE {col1} = 5.25 AND {col2} = 3"
            );
            
            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
        
        [Test]
        [TestCase("Amount", "Chars")]
        public void OrWithConstantExpressions(string col1, string col2)
        {
            var actualSelect = this._sql.From(TableName)
                .Where(ConstantExp(col1) == ConstantExp(5.25) |
                       ConstantExp(col2) == ConstantExp(3))
                .Select()
                .ToString();

            var expectedSelect = Regex.Escape(
                $"SELECT * FROM {TableName}{Environment.NewLine}" +
                $"WHERE {col1} = 5.25 OR {col2} = 3"
            );
            
            Assert.That(Regex.Escape(actualSelect), Is.EqualTo(expectedSelect));
        }
    }
}