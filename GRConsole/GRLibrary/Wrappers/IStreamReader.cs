using System;

namespace DBLibrary.Wrappers
{
    public interface IStreamReader : IDisposable
    {
        string ReadLine();
        void InitializeReader(string path);       
    }
}
