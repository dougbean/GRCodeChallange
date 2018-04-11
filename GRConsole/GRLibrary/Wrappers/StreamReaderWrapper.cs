using System.IO;
using System;

namespace GRLibrary.Wrappers
{
    public class StreamReaderWrapper : IStreamReader, IDisposable 
    {   
        private StreamReader _streamReader;
        
        public void InitializeReader(string path) 
        {
            _streamReader = new StreamReader(path);
        }

        public string ReadLine()
        {
            string line = _streamReader.ReadLine();
            return line;
        }

        public StreamReader Reader
        {
            get { return _streamReader; }
        }

        public void Dispose()
        {
            _streamReader.Dispose();
        }
    }
}
