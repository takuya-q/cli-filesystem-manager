using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class FileShowCommand : ICommand
{
    private const string ConsoleModeName = "console";

    private readonly string _path;
    private readonly string _mode;

    public FileShowCommand(string path, string mode)
    {
        _path = path;
        _mode = mode;
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

        string? normalizedMode = NormalizeMode(_mode);

        if (normalizedMode == null)
        {
            return Result.Failure("Unsupported mode for file show command.");
        }

        ResultWithValue<string> readResult =
            context.FileSystem.ReadFileAsText(localPath);

        if (readResult.IsFailure || readResult.Value == null)
        {
            string errorMessage = readResult.ErrorMessage ?? "Failed to read file.";
            return Result.Failure(errorMessage);
        }

        string content = readResult.Value;

        context.Output.WriteLine(content);

        return Result.Success();
    }

    private string? NormalizeMode(string rawMode)
    {
        if (string.IsNullOrWhiteSpace(rawMode))
        {
            return ConsoleModeName;
        }

        if (string.Equals(rawMode, ConsoleModeName, StringComparison.OrdinalIgnoreCase))
        {
            return ConsoleModeName;
        }

        return null;
    }
}