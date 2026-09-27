using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.FileSystem;

public sealed class LocalFileSystemFactory : IFileSystemFactory
{
    private const string LocalModeName = "local";

    private readonly LocalFileSystemPathBuilder _pathBuilder;

    public LocalFileSystemFactory(LocalFileSystemPathBuilder pathBuilder)
    {
        _pathBuilder = pathBuilder;
    }

    public ResultWithValue<IFileSystem> Create(string address, string mode)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            return ResultWithValue<IFileSystem>.Failure("Address must not be empty.");
        }

        if (!string.Equals(mode, LocalModeName, StringComparison.OrdinalIgnoreCase))
        {
            return ResultWithValue<IFileSystem>.Failure("Unsupported file system mode.");
        }

        try
        {
            string fullRootPath = Path.GetFullPath(address);

            if (!Directory.Exists(fullRootPath))
            {
                return ResultWithValue<IFileSystem>.Failure("Root directory does not exist.");
            }

            var fileSystem = new LocalFileSystem(fullRootPath, _pathBuilder);

            return ResultWithValue<IFileSystem>.Success(fileSystem);
        }
        catch (Exception)
        {
            return ResultWithValue<IFileSystem>.Failure(
                "Failed to create local file system for the specified address.");
        }
    }
}