using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Tree;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public sealed class TreeListCommandFactory : ICommandFactory
{
    private const string TreeKeyword = "tree";
    private const string ListKeyword = "list";

    private readonly IDirectoryTreePrinter _treePrinter;
    private readonly TreeFormattingOptions _formattingOptions;
    private readonly TreeListDepthParser _depthParser;

    public TreeListCommandFactory(
        IDirectoryTreePrinter treePrinter,
        TreeFormattingOptions formattingOptions,
        NumberParser numberParser)
    {
        _treePrinter = treePrinter;
        _formattingOptions = formattingOptions;
        _depthParser = new TreeListDepthParser(numberParser);
    }

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

        bool isList = string.Equals(
            second,
            ListKeyword,
            StringComparison.OrdinalIgnoreCase);

        return isTree && isList;
    }

    public ResultWithValue<ICommand> Create(CommandTokens tokens)
    {
        ResultWithValue<int> depthResult = _depthParser.ParseDepth(tokens);

        if (depthResult.IsFailure)
        {
            string depthError = depthResult.ErrorMessage ?? "Invalid depth value.";
            return ResultWithValue<ICommand>.Failure(depthError);
        }

        int depth = depthResult.Value;

        ICommand command = new TreeListCommand(
            depth,
            _treePrinter,
            _formattingOptions);

        return ResultWithValue<ICommand>.Success(command);
    }
}