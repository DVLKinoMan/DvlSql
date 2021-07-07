using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvlSql.Abstract
{
    public interface IMergeable
    {
        IMergeUsing Merge(string tableName);
    }

    public interface IMergeUsing
    {
        IMergeOn Using();
    }

    public interface IMergeOn
    {

    }

    public interface IMergeMatchable
    {

    }

    public interface IMergeThenable
    {

    }

    public interface IMergeOutput
    {

    }
}
