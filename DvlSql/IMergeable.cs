namespace DvlSql
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
