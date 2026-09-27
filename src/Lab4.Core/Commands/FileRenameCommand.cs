using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class FileRenameCommand : ICommand
{
    private const char PathSeparator = '/';

    private readonly string _path;
    private readonly string _newName;

    public FileRenameCommand(string path, string newName)
    {
        _path = path;
        _newName = newName;
    }

    public Result Execute(CommandContext context)
    {
        if (string.IsNullOrWhiteSpace(_newName))
        {
            return Result.Failure("New name must not be empty.");
        }

        if (HasPathSeparator(_newName))
        {
            return Result.Failure("New name must not contain path separators.");
        }

        ResultWithValue<string> resolveResult =
            context.PathResolver.ResolvePath(context.ApplicationState, _path);

        if (resolveResult.IsFailure || resolveResult.Value == null)
        {
            string message = resolveResult.ErrorMessage ?? "Invalid path.";
            return Result.Failure(message);
        }

        string localPath = resolveResult.Value;

        Result renameResult =
            context.FileSystem.RenameFile(localPath, _newName);

        if (renameResult.IsFailure)
        {
            return renameResult;
        }

        return Result.Success();
    }

    private bool HasPathSeparator(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        return value.Any(character => character == PathSeparator);
    }
}