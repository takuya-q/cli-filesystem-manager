using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Tree;

public interface IFileSystemEntrySorter
{
    IReadOnlyList<FileSystemEntry> Sort(IReadOnlyCollection<FileSystemEntry> entries);
}