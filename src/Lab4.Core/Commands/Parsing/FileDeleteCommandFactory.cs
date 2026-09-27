using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public sealed class FileDeleteCommandFactory : ICommandFactory
{
    private const string FileKeyword = "file";
    private const string DeleteKeyword = "delete";

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

        bool isDelete = string.Equals(
            second,
            DeleteKeyword,
            StringComparison.OrdinalIgnoreCase);

        return isFile && isDelete;
    }

    public ResultWithValue<ICommand> Create(CommandTokens tokens)
    {
        if (tokens.Count < 3)
        {
            return ResultWithValue<ICommand>.Failure(
                "Path argument is required for file delete command.");
        }

        if (tokens.Count > 3)
        {
            return ResultWithValue<ICommand>.Failure(
                "Too many arguments for file delete command.");
        }

        string path = tokens.GetToken(2);

        ICommand command = new FileDeleteCommand(path);

        return ResultWithValue<ICommand>.Success(command);
    }
}