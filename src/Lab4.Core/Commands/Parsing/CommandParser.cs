using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public sealed class CommandParser : ICommandParser
{
    private readonly InputTokenizer _tokenizer;
    private readonly IReadOnlyCollection<ICommandFactory> _factories;

    public CommandParser(
        InputTokenizer tokenizer,
        IReadOnlyCollection<ICommandFactory> factories)
    {
        _tokenizer = tokenizer;
        _factories = factories;
    }

    public ResultWithValue<ICommand> Parse(string input)
    {
        ResultWithValue<CommandTokens> tokensResult = _tokenizer.Tokenize(input);

        if (tokensResult.IsFailure || tokensResult.Value == null)
        {
            string message = tokensResult.ErrorMessage ?? "Invalid command.";
            return ResultWithValue<ICommand>.Failure(message);
        }

        CommandTokens tokens = tokensResult.Value;

        foreach (ICommandFactory factory in _factories)
        {
            if (!factory.CanHandle(tokens))
            {
                continue;
            }

            ResultWithValue<ICommand> commandResult = factory.Create(tokens);

            if (commandResult.IsSuccess)
            {
                return commandResult;
            }

            string error = commandResult.ErrorMessage ?? "Invalid command arguments.";
            return ResultWithValue<ICommand>.Failure(error);
        }

        return ResultWithValue<ICommand>.Failure("Unknown command.");
    }
}