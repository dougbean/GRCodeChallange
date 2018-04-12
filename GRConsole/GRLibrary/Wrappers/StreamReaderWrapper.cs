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
            return  _streamReader.ReadLine();
        }

        public void Dispose()
        {
            _streamReader.Dispose();
        }
    }
}
