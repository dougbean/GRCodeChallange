using System;

namespace GRLibrary.Wrappers
{
    public interface IStreamReader : IDisposable
    {
        string ReadLine();
        void InitializeReader(string path);       
    }
}
