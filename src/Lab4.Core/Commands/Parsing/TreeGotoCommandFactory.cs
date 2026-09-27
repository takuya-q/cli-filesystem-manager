using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public sealed class TreeGotoCommandFactory : ICommandFactory
{
    private const string TreeKeyword = "tree";
    private const string GotoKeyword = "goto";

    public bool CanHandle(CommandTokens tokens)
    {
        if (tokens.Count < 2)
        {
            return false;
        }

        string first = tokens.GetToken(0);
        string second = tokens.GetToken(1);

        bool isTree = string.Equals(
            first,
            TreeKeyword,
            StringComparison.OrdinalIgnoreCase);

        bool isGoto = string.Equals(
            second,
            GotoKeyword,
            StringComparison.OrdinalIgnoreCase);

        return isTree && isGoto;
    }

    public ResultWithValue<ICommand> Create(CommandTokens tokens)
    {
        if (tokens.Count < 3)
        {
            return ResultWithValue<ICommand>.Failure(
                "Path argument is required for tree goto command.");
        }

        if (tokens.Count > 3)
        {
            return ResultWithValue<ICommand>.Failure(
                "Too many arguments for tree goto command.");
        }

        string path = tokens.GetToken(2);

        ICommand command = new TreeGotoCommand(path);

        return ResultWithValue<ICommand>.Success(command);
    }
}