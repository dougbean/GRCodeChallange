using System;
using System.IO;

namespace GRLibrary
{
    public interface IStreamReader : IDisposable
    {
        string ReadLine();
        //void InitializeReader(Stream stream);
        StreamReader Reader { get; }
    }
}
