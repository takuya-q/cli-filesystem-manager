using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public sealed class ConnectCommandFactory : ICommandFactory
{
    private const string CommandName = "connect";

    private readonly IFileSystemFactory _fileSystemFactory;

    public ConnectCommandFactory(IFileSystemFactory fileSystemFactory)
    {
        _fileSystemFactory = fileSystemFactory;
    }

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
        if (tokens.Count < 2)
        {
            return ResultWithValue<ICommand>.Failure("Address argument is required.");
        }

        string address = tokens.GetToken(1);

        string mode = "local";

        bool flagsStarted = false;
        int index = 2;

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
                        return ResultWithValue<ICommand>.Failure("Mode value for -m flag is missing.");
                    }

                    string modeValue = tokens.GetToken(index + 1);
                    mode = modeValue;

                    index += 2;
                    continue;
                }

                return ResultWithValue<ICommand>.Failure("Unknown flag for connect command.");
            }

            if (flagsStarted)
            {
                return ResultWithValue<ICommand>.Failure(
                    "Positional parameters cannot follow flags for connect command.");
            }

            return ResultWithValue<ICommand>.Failure("Too many positional arguments for connect command.");
        }

        ICommand command = new ConnectCommand(address, mode, _fileSystemFactory);

        return ResultWithValue<ICommand>.Success(command);
    }
}