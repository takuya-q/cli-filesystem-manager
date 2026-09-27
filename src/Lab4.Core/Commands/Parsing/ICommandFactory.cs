using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public interface ICommandFactory
{
    bool CanHandle(CommandTokens tokens);

    ResultWithValue<ICommand> Create(CommandTokens tokens);
}