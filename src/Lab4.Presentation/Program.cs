using Itmo.ObjectOrientedProgramming.Lab4.Core.Application;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Output;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Paths;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Tree;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Output;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation;

public static class Program
{
    public static void Main()
    {
        IOutput output = new ConsoleOutput();

        var applicationState = new ApplicationState();
        var pathResolver = new UnixPathResolver();

        IFileSystem initialFileSystem = new DisconnectedFileSystem();

        var context = new CommandContext(
            applicationState,
            initialFileSystem,
            pathResolver,
            output);

        var formattingOptions = new TreeFormattingOptions(
            directoryPrefix: "[D] ",
            filePrefix: "[F] ",
            indent: "  ");

        IDirectoryTreePrinter treePrinter = new DirectoryTreePrinter();

        var numberParser = new NumberParser();

        var pathBuilder = new LocalFileSystemPathBuilder();
        IFileSystemFactory fileSystemFactory = new LocalFileSystemFactory(pathBuilder);

        var tokenizer = new InputTokenizer();

        IReadOnlyCollection<ICommandFactory> factories =
            new List<ICommandFactory>
            {
                new ConnectCommandFactory(fileSystemFactory),
                new DisconnectCommandFactory(),
                new TreeGotoCommandFactory(),
                new TreeListCommandFactory(treePrinter, formattingOptions, numberParser),
                new FileShowCommandFactory(),
                new FileMoveCommandFactory(),
                new FileCopyCommandFactory(),
                new FileDeleteCommandFactory(),
                new FileRenameCommandFactory(),
            };

        ICommandParser parser = new CommandParser(tokenizer, factories);

        RunCommandLoop(parser, context, output);
    }

    private static void RunCommandLoop(
        ICommandParser parser,
        CommandContext context,
        IOutput output)
    {
        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (input is null)
            {
                break;
            }

            string trimmed = input.Trim();

            if (trimmed.Length == 0)
            {
                continue;
            }

            if (string.Equals(trimmed, "exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            ResultWithValue<ICommand> parseResult = parser.Parse(trimmed);

            if (parseResult.IsFailure || parseResult.Value is null)
            {
                string error = parseResult.ErrorMessage ?? "Failed to parse command.";
                output.WriteLine(error);
                continue;
            }

            ICommand command = parseResult.Value;

            Result executionResult = command.Execute(context);

            if (executionResult.IsFailure)
            {
                string error = executionResult.ErrorMessage ?? "Command execution failed.";
                output.WriteLine(error);
            }
        }
    }
}