using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public sealed class InputTokenizer
{
    public ResultWithValue<CommandTokens> Tokenize(string input)
    {
        string trimmed = input.Trim();

        if (trimmed.Length == 0)
        {
            return ResultWithValue<CommandTokens>.Failure("Command is empty.");
        }

        string[] parts = trimmed.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 0)
        {
            return ResultWithValue<CommandTokens>.Failure("Command is empty.");
        }

        IReadOnlyList<string> tokens = parts;

        var commandTokens = new CommandTokens(tokens);

        return ResultWithValue<CommandTokens>.Success(commandTokens);
    }
}