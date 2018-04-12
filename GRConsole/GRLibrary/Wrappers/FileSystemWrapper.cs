using System.IO;

namespace GRLibrary.Wrappers
{    
    public class FileSystemWrapper: IFileSystem
    {
        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }
    }
}
