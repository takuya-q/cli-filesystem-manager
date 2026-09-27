using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public interface ICommandParser
{
    ResultWithValue<ICommand> Parse(string input);
}