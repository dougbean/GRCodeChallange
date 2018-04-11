using System.IO;

namespace GRLibrary.Wrappers
{
    public interface IFileSystem
    {
        string ReadAllText(string fileName);
        string Combine(string path1, string path2);
        bool DirectoryExists(string directory);
        bool FileExists(string fileName);
        void DeleteFile(string fileName);
        bool IsFileLocked(FileInfo file);
    }
}
