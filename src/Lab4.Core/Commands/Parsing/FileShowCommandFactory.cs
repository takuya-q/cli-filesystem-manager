using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public sealed class FileShowCommandFactory : ICommandFactory
{
    private const string FileKeyword = "file";
    private const string ShowKeyword = "show";

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

        bool isShow = string.Equals(
            second,
            ShowKeyword,
            StringComparison.OrdinalIgnoreCase);

        return isFile && isShow;
    }

    public ResultWithValue<ICommand> Create(CommandTokens tokens)
    {
        if (tokens.Count < 3)
        {
            return ResultWithValue<ICommand>.Failure(
                "Path argument is required for file show command.");
        }

        string path = tokens.GetToken(2);

        string mode = "console";
        bool flagsStarted = false;
        int index = 3;

        while (index < tokens.Count)
        {
            string token = tokens.GetToken(index);

            bool isFlag = token.StartsWith('-');

            if (isFlag)
            {
                flagsStarted = true;

                if (string.Equals(token, "-m", StringComparison.Ordinal))
                {
                    if (index + 1 >= tokens.Count)
                    {
                        return ResultWithValue<ICommand>.Failure(
                            "Mode value for -m flag is missing.");
                    }

                    string modeValue = tokens.GetToken(index + 1);
                    mode = modeValue;

                    index += 2;
                    continue;
                }

                return ResultWithValue<ICommand>.Failure(
                    "Unknown flag for file show command.");
            }

            if (flagsStarted)
            {
                return ResultWithValue<ICommand>.Failure(
                    "Positional parameters cannot follow flags for file show command.");
            }

            return ResultWithValue<ICommand>.Failure(
                "Too many positional arguments for file show command.");
        }

        ICommand command = new FileShowCommand(path, mode);

        return ResultWithValue<ICommand>.Success(command);
    }
}