using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public sealed class TreeListDepthParser
{
    private const string DepthFlag = "-d";

    private readonly NumberParser _numberParser;

    public TreeListDepthParser(NumberParser numberParser)
    {
        _numberParser = numberParser;
    }

    public ResultWithValue<int> ParseDepth(CommandTokens tokens)
    {
        if (tokens.Count < 4)
        {
            return ResultWithValue<int>.Failure(
                "Depth flag -d and its value are required for tree list command.");
        }

        int index = 2;
        bool depthFlagFound = false;
        string depthText = string.Empty;

        while (index < tokens.Count)
        {
            string token = tokens.GetToken(index);

            bool isFlag = token.StartsWith('-');

            if (!isFlag)
            {
                return ResultWithValue<int>.Failure(
                    "tree list command does not accept positional arguments after subcommand.");
            }

            if (string.Equals(token, DepthFlag, StringComparison.Ordinal))
            {
                if (depthFlagFound)
                {
                    return ResultWithValue<int>.Failure(
                        "Depth flag -d is specified more than once.");
                }

                if (index + 1 >= tokens.Count)
                {
                    return ResultWithValue<int>.Failure(
                        "Depth value for -d flag is missing.");
                }

                depthText = tokens.GetToken(index + 1);
                depthFlagFound = true;

                index += 2;
                continue;
            }

            return ResultWithValue<int>.Failure(
                "Unknown flag for tree list command.");
        }

        if (!depthFlagFound)
        {
            return ResultWithValue<int>.Failure(
                "Depth flag -d is required for tree list command.");
        }

        ResultWithValue<int> depthResult =
            _numberParser.ParsePositiveInteger(depthText, "Depth");

        if (depthResult.IsFailure || depthResult.Value <= 0)
        {
            string depthError = depthResult.ErrorMessage ?? "Invalid depth value.";
            return ResultWithValue<int>.Failure(depthError);
        }

        int depth = depthResult.Value;

        return ResultWithValue<int>.Success(depth);
    }
}