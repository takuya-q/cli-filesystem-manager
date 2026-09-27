using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public sealed class FileRenameCommandFactory : ICommandFactory
{
    private const string FileKeyword = "file";
    private const string RenameKeyword = "rename";

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

        bool isRename = string.Equals(
            second,
            RenameKeyword,
            StringComparison.OrdinalIgnoreCase);

        return isFile && isRename;
    }

    public ResultWithValue<ICommand> Create(CommandTokens tokens)
    {
        if (tokens.Count < 4)
        {
            return ResultWithValue<ICommand>.Failure(
                "Path and new name arguments are required for file rename command.");
        }

        if (tokens.Count > 4)
        {
            return ResultWithValue<ICommand>.Failure(
                "Too many arguments for file rename command.");
        }

        string path = tokens.GetToken(2);
        string newName = tokens.GetToken(3);

        ICommand command = new FileRenameCommand(path, newName);

        return ResultWithValue<ICommand>.Success(command);
    }
}