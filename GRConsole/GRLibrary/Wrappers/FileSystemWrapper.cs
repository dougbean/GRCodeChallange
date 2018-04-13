using System.IO;

namespace DBLibrary.Wrappers
{    
    public class FileSystemWrapper: IFileSystem
    {
        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }
    }
}
