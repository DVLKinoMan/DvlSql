using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DVL_SQL_Test1.Expressions;

namespace DVL_SQL_Test1.Abstract
{
    public interface IExecuter
    {
        (int, bool) Execute();
        List<TResult> ToList<TResult>(Func<SqlDataReader, TResult> reader);
        Task<List<TResult>> ToListAsync<TResult>(CancellationToken cancellationToken = default);
        TResult FirstOrDefault<TResult>();
    }
}
