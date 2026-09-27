using Itmo.ObjectOrientedProgramming.Lab4.Core.Application;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Paths;

public interface IPathResolver
{
    ResultWithValue<string> ResolvePath(ApplicationState state, string rawPath);
}