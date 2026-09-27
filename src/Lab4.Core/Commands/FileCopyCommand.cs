using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class FileCopyCommand : ICommand
{
    private readonly string _sourcePath;
    private readonly string _destinationPath;

    public FileCopyCommand(string sourcePath, string destinationPath)
    {
        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
    }

    public Result Execute(CommandContext context)
    {
        ResultWithValue<string> sourceResult =
            context.PathResolver.ResolvePath(context.ApplicationState, _sourcePath);

        if (sourceResult.IsFailure || sourceResult.Value == null)
        {
            string message = sourceResult.ErrorMessage ?? "Invalid source path.";
            return Result.Failure(message);
        }

        string sourceLocalPath = sourceResult.Value;

        ResultWithValue<string> destinationResult =
            context.PathResolver.ResolvePath(context.ApplicationState, _destinationPath);

        if (destinationResult.IsFailure || destinationResult.Value == null)
        {
            string message = destinationResult.ErrorMessage ?? "Invalid destination path.";
            return Result.Failure(message);
        }

        string destinationLocalPath = destinationResult.Value;

        Result copyResult =
            context.FileSystem.CopyFile(sourceLocalPath, destinationLocalPath);

        if (copyResult.IsFailure)
        {
            return copyResult;
        }

        return Result.Success();
    }
}