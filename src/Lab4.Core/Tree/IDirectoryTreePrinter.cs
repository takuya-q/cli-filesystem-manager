using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Output;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Tree;

public interface IDirectoryTreePrinter
{
    Result PrintTree(
        IFileSystem fileSystem,
        string rootLocalPath,
        int maxDepth,
        TreeFormattingOptions formattingOptions,
        IOutput output);
}