using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.FileSystem;

public sealed class LocalFileSystem : IFileSystem
{
    private readonly string _rootPath;
    private readonly LocalFileSystemPathBuilder _pathBuilder;

    public LocalFileSystem(string rootPath, LocalFileSystemPathBuilder pathBuilder)
    {
        _rootPath = rootPath;
        _pathBuilder = pathBuilder;
    }

    public ResultWithValue<IReadOnlyCollection<FileSystemEntry>> GetDirectoryEntries(string localDirectoryPath)
    {
        string fullPath = _pathBuilder.BuildFullPath(_rootPath, localDirectoryPath);

        if (!Directory.Exists(fullPath))
        {
            return ResultWithValue<IReadOnlyCollection<FileSystemEntry>>.Failure(
                "Directory does not exist.");
        }

        try
        {
            var entries = new List<FileSystemEntry>();

            foreach (string directoryPath in Directory.EnumerateDirectories(fullPath))
            {
                string name = Path.GetFileName(directoryPath);
                string entryLocalPath = _pathBuilder.BuildLocalPath(localDirectoryPath, name);
                entries.Add(new FileSystemEntry(name, entryLocalPath, isDirectory: true));
            }

            foreach (string filePath in Directory.EnumerateFiles(fullPath))
            {
                string name = Path.GetFileName(filePath);
                string entryLocalPath = _pathBuilder.BuildLocalPath(localDirectoryPath, name);
                entries.Add(new FileSystemEntry(name, entryLocalPath, isDirectory: false));
            }

            IReadOnlyCollection<FileSystemEntry> resultEntries = entries;

            return ResultWithValue<IReadOnlyCollection<FileSystemEntry>>.Success(resultEntries);
        }
        catch (Exception)
        {
            return ResultWithValue<IReadOnlyCollection<FileSystemEntry>>.Failure(
                "Failed to read directory.");
        }
    }

    public ResultWithValue<string> ReadFileAsText(string localFilePath)
    {
        string fullPath = _pathBuilder.BuildFullPath(_rootPath, localFilePath);

        if (!File.Exists(fullPath))
        {
            return ResultWithValue<string>.Failure("File does not exist.");
        }

        try
        {
            string content = File.ReadAllText(fullPath);
            return ResultWithValue<string>.Success(content);
        }
        catch (Exception)
        {
            return ResultWithValue<string>.Failure("Failed to read file.");
        }
    }

    public Result CopyFile(string sourceFileLocalPath, string destinationDirectoryLocalPath)
    {
        string sourceFullPath = _pathBuilder.BuildFullPath(_rootPath, sourceFileLocalPath);
        string destinationDirectoryFullPath =
            _pathBuilder.BuildFullPath(_rootPath, destinationDirectoryLocalPath);

        if (!File.Exists(sourceFullPath))
        {
            return Result.Failure("Source file does not exist.");
        }

        if (!Directory.Exists(destinationDirectoryFullPath))
        {
            return Result.Failure("Destination directory does not exist.");
        }

        string fileName = Path.GetFileName(sourceFullPath);
        string destinationFileFullPath = Path.Combine(destinationDirectoryFullPath, fileName);

        if (File.Exists(destinationFileFullPath))
        {
            return Result.Failure("File with the same name already exists in destination directory.");
        }

        try
        {
            File.Copy(sourceFullPath, destinationFileFullPath);
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure("Failed to copy file.");
        }
    }

    public Result MoveFile(string sourceFileLocalPath, string destinationDirectoryLocalPath)
    {
        string sourceFullPath = _pathBuilder.BuildFullPath(_rootPath, sourceFileLocalPath);
        string destinationDirectoryFullPath =
            _pathBuilder.BuildFullPath(_rootPath, destinationDirectoryLocalPath);

        if (!File.Exists(sourceFullPath))
        {
            return Result.Failure("Source file does not exist.");
        }

        if (!Directory.Exists(destinationDirectoryFullPath))
        {
            return Result.Failure("Destination directory does not exist.");
        }

        string fileName = Path.GetFileName(sourceFullPath);
        string destinationFileFullPath = Path.Combine(destinationDirectoryFullPath, fileName);

        if (File.Exists(destinationFileFullPath))
        {
            return Result.Failure("File with the same name already exists in destination directory.");
        }

        try
        {
            File.Move(sourceFullPath, destinationFileFullPath);
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure("Failed to move file.");
        }
    }

    public Result DeleteFile(string localFilePath)
    {
        string fullPath = _pathBuilder.BuildFullPath(_rootPath, localFilePath);

        if (!File.Exists(fullPath))
        {
            return Result.Failure("File does not exist.");
        }

        try
        {
            File.Delete(fullPath);
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure("Failed to delete file.");
        }
    }

    public Result RenameFile(string localFilePath, string newName)
    {
        string fullPath = _pathBuilder.BuildFullPath(_rootPath, localFilePath);

        if (!File.Exists(fullPath))
        {
            return Result.Failure("File does not exist.");
        }

        string directoryPath = Path.GetDirectoryName(fullPath) ?? _rootPath;
        string destinationFullPath = Path.Combine(directoryPath, newName);

        if (File.Exists(destinationFullPath) || Directory.Exists(destinationFullPath))
        {
            return Result.Failure("File or directory with the same name already exists.");
        }

        try
        {
            File.Move(fullPath, destinationFullPath);
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure("Failed to rename file.");
        }
    }
}