using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Tree;

public interface IDirectoryTreeLineFormatter
{
    string FormatDirectoryLine(
        string currentIndent,
        string localPath,
        TreeFormattingOptions formattingOptions);

    string FormatFileLine(
        string currentIndent,
        FileSystemEntry entry,
        TreeFormattingOptions formattingOptions);
}