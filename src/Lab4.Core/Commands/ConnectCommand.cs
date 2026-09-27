using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class ConnectCommand : ICommand
{
    private const string LocalModeName = "local";

    private readonly string _address;
    private readonly string _mode;
    private readonly IFileSystemFactory _fileSystemFactory;

    public ConnectCommand(string address, string mode, IFileSystemFactory fileSystemFactory)
    {
        _address = address;
        _mode = string.IsNullOrWhiteSpace(mode) ? LocalModeName : mode;
        _fileSystemFactory = fileSystemFactory;
    }

    public Result Execute(CommandContext context)
    {
        if (context.ApplicationState.IsConnected)
        {
            return Result.Failure("File system is already connected.");
        }

        if (!IsAddressAbsolute(_address))
        {
            return Result.Failure("Address must be an absolute path.");
        }

        string? normalizedMode = NormalizeMode(_mode);

        if (normalizedMode == null)
        {
            return Result.Failure("Unsupported file system mode.");
        }

        ResultWithValue<IFileSystem> fileSystemResult = _fileSystemFactory.Create(_address, normalizedMode);

        if (fileSystemResult.IsFailure)
        {
            return Result.Failure(
                fileSystemResult.ErrorMessage ?? "Failed to create file system.");
        }

        IFileSystem? fileSystem = fileSystemResult.Value;

        if (fileSystem == null)
        {
            return Result.Failure("File system factory returned empty result.");
        }

        Result stateResult = context.ApplicationState.Connect(_address);

        if (stateResult.IsFailure)
        {
            return stateResult;
        }

        context.SetFileSystem(fileSystem);

        return Result.Success();
    }

    private bool IsAddressAbsolute(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return value.StartsWith('/');
    }

    private string? NormalizeMode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return LocalModeName;
        }

        if (string.Equals(value, LocalModeName, StringComparison.OrdinalIgnoreCase))
        {
            return LocalModeName;
        }

        return null;
    }
}