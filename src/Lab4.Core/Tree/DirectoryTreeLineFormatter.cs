using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Tree;

public sealed class DirectoryTreeLineFormatter : IDirectoryTreeLineFormatter
{
    private const char PathSeparator = '/';

    public string FormatDirectoryLine(
        string currentIndent,
        string localPath,
        TreeFormattingOptions formattingOptions)
    {
        string directoryName = GetDirectoryDisplayName(localPath);

        return currentIndent
               + formattingOptions.DirectoryPrefix
               + directoryName;
    }

    public string FormatFileLine(
        string currentIndent,
        FileSystemEntry entry,
        TreeFormattingOptions formattingOptions)
    {
        return currentIndent
               + formattingOptions.Indent
               + formattingOptions.FilePrefix
               + entry.Name;
    }

    private string GetDirectoryDisplayName(string localPath)
    {
        if (string.IsNullOrEmpty(localPath))
        {
            return ".";
        }

        int lastSeparatorIndex = localPath.LastIndexOf(PathSeparator);

        if (lastSeparatorIndex < 0 || lastSeparatorIndex == localPath.Length - 1)
        {
            return localPath;
        }

        string name = localPath.Substring(lastSeparatorIndex + 1);

        return name;
    }
}