using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public sealed class FileMoveCommandFactory : ICommandFactory
{
    private const string FileKeyword = "file";
    private const string MoveKeyword = "move";

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

        bool isMove = string.Equals(
            second,
            MoveKeyword,
            StringComparison.OrdinalIgnoreCase);

        return isFile && isMove;
    }

    public ResultWithValue<ICommand> Create(CommandTokens tokens)
    {
        if (tokens.Count < 4)
        {
            return ResultWithValue<ICommand>.Failure(
                "Source and destination path arguments are required for file move command.");
        }

        if (tokens.Count > 4)
        {
            return ResultWithValue<ICommand>.Failure(
                "Too many arguments for file move command.");
        }

        string sourcePath = tokens.GetToken(2);
        string destinationPath = tokens.GetToken(3);

        ICommand command = new FileMoveCommand(sourcePath, destinationPath);

        return ResultWithValue<ICommand>.Success(command);
    }
}