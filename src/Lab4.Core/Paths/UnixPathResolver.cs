using Itmo.ObjectOrientedProgramming.Lab4.Core.Application;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Paths;

public sealed class UnixPathResolver : IPathResolver
{
    private const char PathSeparator = '/';

    public ResultWithValue<string> ResolvePath(ApplicationState state, string rawPath)
    {
        if (!state.IsConnected)
        {
            return ResultWithValue<string>.Failure("File system is not connected.");
        }

        string trimmedPath = rawPath.Trim();

        if (trimmedPath.Length == 0)
        {
            return ResultWithValue<string>.Success(state.LocalPath);
        }

        var segments = new List<string>();

        bool isAbsolute = trimmedPath.StartsWith(PathSeparator);

        if (!isAbsolute && !string.IsNullOrEmpty(state.LocalPath))
        {
            segments.AddRange(
                state.LocalPath.Split(
                    PathSeparator,
                    StringSplitOptions.RemoveEmptyEntries));
        }

        string[] rawSegments = trimmedPath.Split(
            PathSeparator,
            StringSplitOptions.RemoveEmptyEntries);

        foreach (string segment in rawSegments)
        {
            if (segment == ".")
            {
                continue;
            }

            if (segment == "..")
            {
                if (segments.Count == 0)
                {
                    return ResultWithValue<string>.Failure("Path goes outside of connection root.");
                }

                segments.RemoveAt(segments.Count - 1);
                continue;
            }

            segments.Add(segment);
        }

        string localPath = string.Join(PathSeparator, segments);

        return ResultWithValue<string>.Success(localPath);
    }
}