using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Tree;

public sealed class DirectoryFirstFileSystemEntrySorter : IFileSystemEntrySorter
{
    public IReadOnlyList<FileSystemEntry> Sort(IReadOnlyCollection<FileSystemEntry> entries)
    {
        var result = new List<FileSystemEntry>(entries);

        result.Sort(CompareEntries);

        return result;
    }

    private int CompareEntries(FileSystemEntry first, FileSystemEntry second)
    {
        if (first.IsDirectory && !second.IsDirectory)
        {
            return -1;
        }

        if (!first.IsDirectory && second.IsDirectory)
        {
            return 1;
        }

        string firstName = first.Name;
        string secondName = second.Name;

        return string.Compare(
            firstName,
            secondName,
            StringComparison.OrdinalIgnoreCase);
    }
}