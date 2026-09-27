using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public sealed class DisconnectCommandFactory : ICommandFactory
{
    private const string CommandName = "disconnect";

    public bool CanHandle(CommandTokens tokens)
    {
        if (tokens.Count == 0)
        {
            return false;
        }

        string first = tokens.GetToken(0);

        return string.Equals(first, CommandName, StringComparison.OrdinalIgnoreCase);
    }

    public ResultWithValue<ICommand> Create(CommandTokens tokens)
    {
        if (tokens.Count > 1)
        {
            return ResultWithValue<ICommand>.Failure("disconnect command does not accept arguments.");
        }

        ICommand command = new DisconnectCommand();

        return ResultWithValue<ICommand>.Success(command);
    }
}