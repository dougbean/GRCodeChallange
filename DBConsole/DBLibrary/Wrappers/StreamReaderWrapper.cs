using System.IO;

namespace DBLibrary.Wrappers
{
    public class StreamReaderWrapper : IStreamReader
    {   
        private StreamReader _streamReader;
        
        public void CreateReader(string path) 
        {
            _streamReader = new StreamReader(path);
        }

        public string ReadLine()
        {
            return  _streamReader.ReadLine();
        }

        public void DisposeReader()
        {
            _streamReader.Dispose();
            _streamReader = null;
        }
    }
}
