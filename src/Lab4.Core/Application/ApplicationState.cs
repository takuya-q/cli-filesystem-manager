using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Application;

public class ApplicationState
{
    public string? ConnectionRootPath { get; private set; }

    public string LocalPath { get; private set; } = string.Empty;

    public bool IsConnected => ConnectionRootPath != null;

    public Result Connect(string absoluteRootPath)
    {
        if (string.IsNullOrWhiteSpace(absoluteRootPath))
        {
            return Result.Failure("Root path must not be empty.");
        }

        ConnectionRootPath = absoluteRootPath;
        LocalPath = string.Empty;

        return Result.Success();
    }

    public Result Disconnect()
    {
        if (!IsConnected)
        {
            return Result.Failure("File system is not connected.");
        }

        ConnectionRootPath = null;
        LocalPath = string.Empty;

        return Result.Success();
    }

    public Result SetLocalPath(string localPath)
    {
        if (!IsConnected)
        {
            return Result.Failure("File system is not connected.");
        }

        LocalPath = localPath;

        return Result.Success();
    }
}