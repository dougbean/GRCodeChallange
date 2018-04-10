using System.IO;
using System;

namespace GRLibrary
{
    public class StreamReaderWrapper : IStreamReader, IDisposable //todo: make sure I'm using this interface correctly.
    {   
        private StreamReader _streamReader;

        //04092018//
        public StreamReaderWrapper(string path)
        {
            _streamReader = new StreamReader(path);
        }
        //04092018//

        //public void InitializeReader(Stream stream) //todo: Remove this?
        //{
        //    _streamReader = new StreamReader(stream);
        //}

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
