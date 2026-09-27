using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

public sealed class DisconnectedFileSystem : IFileSystem
{
    private const string NotConnectedMessage = "File system is not connected.";

    public ResultWithValue<IReadOnlyCollection<FileSystemEntry>> GetDirectoryEntries(string localDirectoryPath)
    {
        return ResultWithValue<IReadOnlyCollection<FileSystemEntry>>.Failure(NotConnectedMessage);
    }

    public ResultWithValue<string> ReadFileAsText(string localFilePath)
    {
        return ResultWithValue<string>.Failure(NotConnectedMessage);
    }

    public Result CopyFile(string sourceFileLocalPath, string destinationDirectoryLocalPath)
    {
        return Result.Failure(NotConnectedMessage);
    }

    public Result MoveFile(string sourceFileLocalPath, string destinationDirectoryLocalPath)
    {
        return Result.Failure(NotConnectedMessage);
    }

    public Result DeleteFile(string localFilePath)
    {
        return Result.Failure(NotConnectedMessage);
    }

    public Result RenameFile(string localFilePath, string newName)
    {
        return Result.Failure(NotConnectedMessage);
    }
}