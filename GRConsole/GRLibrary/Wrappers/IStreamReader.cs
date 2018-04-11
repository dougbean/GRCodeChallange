using System;
using System.IO;

namespace GRLibrary.Wrappers
{
    public interface IStreamReader : IDisposable
    {
        string ReadLine();
        void InitializeReader(string path);
        StreamReader Reader { get; } //do I need this method?
    }
}
