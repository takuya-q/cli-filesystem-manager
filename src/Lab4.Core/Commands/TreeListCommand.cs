using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Tree;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class TreeListCommand : ICommand
{
    private readonly int _depth;
    private readonly IDirectoryTreePrinter _treePrinter;
    private readonly TreeFormattingOptions _formattingOptions;

    public TreeListCommand(
        int depth,
        IDirectoryTreePrinter treePrinter,
        TreeFormattingOptions formattingOptions)
    {
        _depth = depth;
        _treePrinter = treePrinter;
        _formattingOptions = formattingOptions;
    }

    public Result Execute(CommandContext context)
    {
        if (_depth <= 0)
        {
            return Result.Failure("Depth must be greater than zero.");
        }

        string rootLocalPath = context.ApplicationState.LocalPath;

        Result printResult = _treePrinter.PrintTree(
            context.FileSystem,
            rootLocalPath,
            _depth,
            _formattingOptions,
            context.Output);

        if (printResult.IsFailure)
        {
            return printResult;
        }

        return Result.Success();
    }
}