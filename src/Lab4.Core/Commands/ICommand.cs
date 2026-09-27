using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public interface ICommand
{
    Result Execute(CommandContext context);
}