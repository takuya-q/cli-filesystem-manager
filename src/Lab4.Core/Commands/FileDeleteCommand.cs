using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class FileDeleteCommand : ICommand
{
    private readonly string _path;

    public FileDeleteCommand(string path)
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

        Result deleteResult =
            context.FileSystem.DeleteFile(localPath);

        if (deleteResult.IsFailure)
        {
            return deleteResult;
        }

        return Result.Success();
    }
}