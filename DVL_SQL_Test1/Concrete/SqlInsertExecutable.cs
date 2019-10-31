using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DVL_SQL_Test1.Abstract;

namespace DVL_SQL_Test1.Concrete
{
    public class SqlInsertExecutable : IInsertExecutable
    {
        public (int, bool) ExecuteAsync(int? timeout = default, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
