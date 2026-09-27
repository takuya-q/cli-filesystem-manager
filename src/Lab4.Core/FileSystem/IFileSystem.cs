using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

public interface IFileSystem
{
    ResultWithValue<IReadOnlyCollection<FileSystemEntry>> GetDirectoryEntries(string localDirectoryPath);

    ResultWithValue<string> ReadFileAsText(string localFilePath);

    Result CopyFile(string sourceFileLocalPath, string destinationDirectoryLocalPath);

    Result MoveFile(string sourceFileLocalPath, string destinationDirectoryLocalPath);

    Result DeleteFile(string localFilePath);

    Result RenameFile(string localFilePath, string newName);
}