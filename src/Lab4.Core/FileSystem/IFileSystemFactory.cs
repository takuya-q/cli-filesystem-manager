using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

public interface IFileSystemFactory
{
    ResultWithValue<IFileSystem> Create(string address, string mode);
}