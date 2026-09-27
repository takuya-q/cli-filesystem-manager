using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Output;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Tree;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public class CommandParserTests
{
    private readonly CommandParser _parser;

    public CommandParserTests()
    {
        var tokenizer = new InputTokenizer();
        var fileSystemFactory = new StubFileSystemFactory();

        var formattingOptions = new TreeFormattingOptions(
            directoryPrefix: "[D] ",
            filePrefix: "[F] ",
            indent: "  ");

        var numberParser = new NumberParser();
        var treePrinter = new StubDirectoryTreePrinter();

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

        _parser = new CommandParser(tokenizer, factories);
    }

    internal sealed class StubFileSystemFactory : IFileSystemFactory
    {
        public ResultWithValue<IFileSystem> Create(string address, string mode)
        {
            return ResultWithValue<IFileSystem>.Failure("Not used in parser tests.");
        }
    }

    internal sealed class StubDirectoryTreePrinter : IDirectoryTreePrinter
    {
        public Result PrintTree(
            IFileSystem fileSystem,
            string rootLocalPath,
            int maxDepth,
            TreeFormattingOptions formattingOptions,
            IOutput output)
        {
            return Result.Success();
        }
    }

    [Fact]
    public void Parse_ConnectWithAddressAndMode_ShouldReturnConnectCommand()
    {
        const string input = "connect /root/path -m local";
        ResultWithValue<ICommand> result = _parser.Parse(input);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsType<ConnectCommand>(result.Value);
    }

    [Fact]
    public void Parse_ConnectWithoutAddress_ShouldReturnFailure()
    {
        const string input = "connect";

        ResultWithValue<ICommand> result = _parser.Parse(input);

        Assert.True(result.IsFailure);
        Assert.Null(result.Value);
    }

    [Fact]
    public void Parse_Disconnect_ShouldReturnDisconnectCommand()
    {
        const string input = "disconnect";

        ResultWithValue<ICommand> result = _parser.Parse(input);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsType<DisconnectCommand>(result.Value);
    }

    [Fact]
    public void Parse_TreeGotoWithPath_ShouldReturnTreeGotoCommand()
    {
        const string input = "tree goto dir/subdir";

        ResultWithValue<ICommand> result = _parser.Parse(input);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsType<TreeGotoCommand>(result.Value);
    }

    [Fact]
    public void Parse_TreeListWithDepthFlag_ShouldReturnTreeListCommand()
    {
        const string input = "tree list -d 3";

        ResultWithValue<ICommand> result = _parser.Parse(input);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsType<TreeListCommand>(result.Value);
    }

    [Fact]
    public void Parse_TreeListWithoutDepthFlag_ShouldReturnFailure()
    {
        const string input = "tree list";

        ResultWithValue<ICommand> result = _parser.Parse(input);

        Assert.True(result.IsFailure);
        Assert.Null(result.Value);
    }

    [Fact]
    public void Parse_FileShowWithPath_ShouldReturnFileShowCommand()
    {
        const string input = "file show file.txt";

        ResultWithValue<ICommand> result = _parser.Parse(input);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsType<FileShowCommand>(result.Value);
    }

    [Fact]
    public void Parse_FileMoveWithTwoPaths_ShouldReturnFileMoveCommand()
    {
        const string input = "file move source.txt destDir";

        ResultWithValue<ICommand> result = _parser.Parse(input);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsType<FileMoveCommand>(result.Value);
    }

    [Fact]
    public void Parse_FileCopyWithTwoPaths_ShouldReturnFileCopyCommand()
    {
        const string input = "file copy source.txt destDir";

        ResultWithValue<ICommand> result = _parser.Parse(input);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsType<FileCopyCommand>(result.Value);
    }

    [Fact]
    public void Parse_FileDeleteWithPath_ShouldReturnFileDeleteCommand()
    {
        const string input = "file delete file.txt";

        ResultWithValue<ICommand> result = _parser.Parse(input);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsType<FileDeleteCommand>(result.Value);
    }

    [Fact]
    public void Parse_FileRenameWithPathAndName_ShouldReturnFileRenameCommand()
    {
        const string input = "file rename file.txt newname.txt";

        ResultWithValue<ICommand> result = _parser.Parse(input);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsType<FileRenameCommand>(result.Value);
    }

    [Fact]
    public void Parse_UnknownCommand_ShouldReturnFailure()
    {
        const string input = "unknown something";

        ResultWithValue<ICommand> result = _parser.Parse(input);

        Assert.True(result.IsFailure);
        Assert.Null(result.Value);
    }
}