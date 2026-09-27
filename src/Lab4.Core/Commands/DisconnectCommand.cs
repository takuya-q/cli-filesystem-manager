using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class DisconnectCommand : ICommand
{
    public Result Execute(CommandContext context)
    {
        if (!context.ApplicationState.IsConnected)
        {
            return Result.Failure("File system is not connected.");
        }

        Result stateResult = context.ApplicationState.Disconnect();

        if (stateResult.IsFailure)
        {
            return stateResult;
        }

        context.SetFileSystem(new DisconnectedFileSystem());

        return Result.Success();
    }
}