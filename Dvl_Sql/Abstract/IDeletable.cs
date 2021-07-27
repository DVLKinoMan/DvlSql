using System;
using System.Collections.Generic;
using System.Data;
using DvlSql.Expressions;
using DvlSql.Models;

namespace DvlSql.Abstract
{
    public interface IDeletable : IDeleteOutputable<int>, IInsertDeleteExecutable<int>
    {
        IDeleteOutputable<TResult> Output<TResult>(Func<IDataReader, TResult> reader, params string[] cols);
    }

    public interface IDeleteOutputable<TResult>
    {
        IInsertDeleteExecutable<TResult> Where(DvlSqlBinaryExpression binaryExpression);
        IInsertDeleteExecutable<TResult> Where(DvlSqlBinaryExpression binaryExpression, IEnumerable<DvlSqlParameter> @params);
    }
}
