using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public sealed class FileCopyCommandFactory : ICommandFactory
{
    private const string FileKeyword = "file";
    private const string CopyKeyword = "copy";

    public bool CanHandle(CommandTokens tokens)
    {
        if (tokens.Count < 2)
        {
            return false;
        }

        string first = tokens.GetToken(0);
        string second = tokens.GetToken(1);

        bool isFile = string.Equals(
            first,
            FileKeyword,
            StringComparison.OrdinalIgnoreCase);

        bool isCopy = string.Equals(
            second,
            CopyKeyword,
            StringComparison.OrdinalIgnoreCase);

        return isFile && isCopy;
    }

    public ResultWithValue<ICommand> Create(CommandTokens tokens)
    {
        if (tokens.Count < 4)
        {
            return ResultWithValue<ICommand>.Failure(
                "Source and destination path arguments are required for file copy command.");
        }

        if (tokens.Count > 4)
        {
            return ResultWithValue<ICommand>.Failure(
                "Too many arguments for file copy command.");
        }

        string sourcePath = tokens.GetToken(2);
        string destinationPath = tokens.GetToken(3);

        ICommand command = new FileCopyCommand(sourcePath, destinationPath);

        return ResultWithValue<ICommand>.Success(command);
    }
}