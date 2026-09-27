using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Output;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Tree;

public sealed class DirectoryTreePrinter : IDirectoryTreePrinter
{
    private readonly IFileSystemEntrySorter _entrySorter;
    private readonly IDirectoryTreeLineFormatter _lineFormatter;

    public DirectoryTreePrinter()
        : this(
            new DirectoryFirstFileSystemEntrySorter(),
            new DirectoryTreeLineFormatter())
    { }

    public DirectoryTreePrinter(
        IFileSystemEntrySorter entrySorter,
        IDirectoryTreeLineFormatter lineFormatter)
    {
        _entrySorter = entrySorter;
        _lineFormatter = lineFormatter;
    }

    public Result PrintTree(
        IFileSystem fileSystem,
        string rootLocalPath,
        int maxDepth,
        TreeFormattingOptions formattingOptions,
        IOutput output)
    {
        if (maxDepth <= 0)
        {
            return Result.Failure("Depth must be greater than zero.");
        }

        string normalizedRootPath = rootLocalPath;

        return PrintDirectory(
            fileSystem,
            normalizedRootPath,
            0,
            maxDepth,
            formattingOptions,
            string.Empty,
            output);
    }

    private Result PrintDirectory(
        IFileSystem fileSystem,
        string localPath,
        int currentDepth,
        int maxDepth,
        TreeFormattingOptions formattingOptions,
        string currentIndent,
        IOutput output)
    {
        ResultWithValue<IReadOnlyCollection<FileSystemEntry>> entriesResult =
            fileSystem.GetDirectoryEntries(localPath);

        if (entriesResult.IsFailure)
        {
            string message = entriesResult.ErrorMessage ?? "Failed to read directory.";
            return Result.Failure(message);
        }

        IReadOnlyCollection<FileSystemEntry>? entries = entriesResult.Value;

        string headerLine = _lineFormatter.FormatDirectoryLine(
            currentIndent,
            localPath,
            formattingOptions);

        output.WriteLine(headerLine);

        if (currentDepth >= maxDepth)
        {
            return Result.Success();
        }

        IReadOnlyCollection<FileSystemEntry> safeEntries =
            entries ?? Array.Empty<FileSystemEntry>();

        IReadOnlyList<FileSystemEntry> sortedEntries = _entrySorter.Sort(safeEntries);

        foreach (FileSystemEntry entry in sortedEntries)
        {
            if (entry.IsDirectory)
            {
                string childIndent = currentIndent + formattingOptions.Indent;

                Result childResult = PrintDirectory(
                    fileSystem,
                    entry.LocalPath,
                    currentDepth + 1,
                    maxDepth,
                    formattingOptions,
                    childIndent,
                    output);

                if (childResult.IsFailure)
                {
                    return childResult;
                }
            }
            else
            {
                string line = _lineFormatter.FormatFileLine(
                    currentIndent,
                    entry,
                    formattingOptions);

                output.WriteLine(line);
            }
        }

        return Result.Success();
    }
}