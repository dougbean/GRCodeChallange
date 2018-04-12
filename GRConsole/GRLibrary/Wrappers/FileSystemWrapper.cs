using System.IO;

namespace GRLibrary.Wrappers
{    
    public class FileSystemWrapper: IFileSystem
    {
        //public string ReadAllText(string fileName)
        //{
        //    return File.ReadAllText(fileName);
        //}

        //public string Combine(string path1, string path2)
        //{
        //    return Path.Combine(path1, path2);
        //}

        //public bool DirectoryExists(string directory)
        //{
        //    return Directory.Exists(directory);
        //}

        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        //public void DeleteFile(string fileName)
        //{
        //    File.Delete(fileName); 
        //}

        //public bool IsFileLocked(FileInfo file)
        //{
        //    FileStream stream = null;
        //    try
        //    {
        //        stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        //    }
        //    catch (IOException)
        //    {
        //        return true;
        //    }
        //    finally
        //    {
        //        if (stream != null)
        //            stream.Close();
        //    }
        //    return false;
        //}
    }
}
