namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.FileSystem;

public sealed class LocalFileSystemPathBuilder
{
    private const char CorePathSeparator = '/';

    public string BuildFullPath(string rootPath, string localPath)
    {
        string safeRoot = rootPath;
        string safeLocal = localPath;

        if (safeLocal.Length == 0)
        {
            return safeRoot;
        }

        string[] segments = safeLocal.Split(
            CorePathSeparator,
            StringSplitOptions.RemoveEmptyEntries);

        string current = safeRoot;

        foreach (string segment in segments)
        {
            current = Path.Combine(current, segment);
        }

        return current;
    }

    public string BuildLocalPath(string parentLocalPath, string name)
    {
        string safeParent = parentLocalPath;
        string safeName = name;

        if (safeParent.Length == 0)
        {
            return safeName;
        }

        if (safeName.Length == 0)
        {
            return safeParent;
        }

        return safeParent + CorePathSeparator + safeName;
    }
}