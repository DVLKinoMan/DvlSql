﻿using DvlSql.Expressions;

namespace DvlSql;

// ReSharper disable once IdentifierTypo
public interface IJoinable
{
    ISelector Join<T>(string tableName, DvlSqlComparisonExpression<T> compExpression);
    ISelector Join(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    ISelector FullJoin<T>(string tableName, DvlSqlComparisonExpression<T> compExpression);
    ISelector FullJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    ISelector LeftJoin<T>(string tableName, DvlSqlComparisonExpression<T> compExpression);
    ISelector LeftJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
    ISelector RightJoin<T>(string tableName, DvlSqlComparisonExpression<T> compExpression);
    ISelector RightJoin(string tableName, string firstTableMatchingCol, string secondTableMatchingCol);
}
