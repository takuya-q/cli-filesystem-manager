namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

public sealed class FileSystemEntry
{
    public string Name { get; }

    public string LocalPath { get; }

    public bool IsDirectory { get; }

    public bool IsFile => !IsDirectory;

    public FileSystemEntry(string name, string localPath, bool isDirectory)
    {
        Name = string.IsNullOrWhiteSpace(name) ? string.Empty : name;
        LocalPath = localPath ?? string.Empty;
        IsDirectory = isDirectory;
    }
}