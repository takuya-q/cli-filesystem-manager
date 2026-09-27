using Itmo.ObjectOrientedProgramming.Lab4.Core.Application;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Output;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Paths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class CommandContext
{
    public ApplicationState ApplicationState { get; }

    public IFileSystem FileSystem { get; private set; }

    public IPathResolver PathResolver { get; }

    public IOutput Output { get; }

    public CommandContext(
        ApplicationState applicationState,
        IFileSystem fileSystem,
        IPathResolver pathResolver,
        IOutput output)
    {
        ApplicationState = applicationState;
        FileSystem = fileSystem;
        PathResolver = pathResolver;
        Output = output;
    }

    public void SetFileSystem(IFileSystem fileSystem)
    {
        FileSystem = fileSystem;
    }
}