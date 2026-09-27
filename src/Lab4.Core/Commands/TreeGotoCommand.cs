using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class TreeGotoCommand : ICommand
{
    private readonly string _path;

    public TreeGotoCommand(string path)
    {
        _path = path;
    }

    public Result Execute(CommandContext context)
    {
        ResultWithValue<string> resolveResult =
            context.PathResolver.ResolvePath(context.ApplicationState, _path);

        if (resolveResult.IsFailure || resolveResult.Value == null)
        {
            string message = resolveResult.ErrorMessage ?? "Invalid path.";
            return Result.Failure(message);
        }

        string localPath = resolveResult.Value;

        ResultWithValue<IReadOnlyCollection<FileSystemEntry>> directoryResult =
            context.FileSystem.GetDirectoryEntries(localPath);

        if (directoryResult.IsFailure)
        {
            string errorMessage = directoryResult.ErrorMessage ?? "Directory does not exist.";
            return Result.Failure(errorMessage);
        }

        Result setPathResult = context.ApplicationState.SetLocalPath(localPath);

        if (setPathResult.IsFailure)
        {
            return setPathResult;
        }

        return Result.Success();
    }
}