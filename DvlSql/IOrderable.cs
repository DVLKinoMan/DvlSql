namespace DvlSql;

// ReSharper disable once IdentifierTypo
public interface IOrderable
{
    IOrderExecutable OrderBy(params string[] fields);
    IOrderExecutable OrderByDescending(params string[] fields);
    ISelectExecutable Skip(int offsetRows, int? fetchNextRows = null);
}
