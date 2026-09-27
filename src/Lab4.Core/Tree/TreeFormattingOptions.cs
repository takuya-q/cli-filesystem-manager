namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Tree;

public sealed class TreeFormattingOptions
{
    public string DirectoryPrefix { get; }

    public string FilePrefix { get; }

    public string Indent { get; }

    public TreeFormattingOptions(
        string directoryPrefix,
        string filePrefix,
        string indent)
    {
        DirectoryPrefix = directoryPrefix;
        FilePrefix = filePrefix;
        Indent = indent;
    }
}