namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public sealed class CommandTokens
{
    public IReadOnlyList<string> Tokens { get; }

    public int Count => Tokens.Count;

    public CommandTokens(IReadOnlyList<string> tokens)
    {
        Tokens = tokens;
    }

    public string GetToken(int index)
    {
        if (index < 0 || index >= Tokens.Count)
        {
            return string.Empty;
        }

        return Tokens[index];
    }
}