using Itmo.ObjectOrientedProgramming.Lab4.Core.Output;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Output;

public sealed class ConsoleOutput : IOutput
{
    public void WriteLine(string text)
    {
        Console.WriteLine(text);
    }
}