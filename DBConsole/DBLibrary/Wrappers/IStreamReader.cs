
namespace DBLibrary.Wrappers
{
    public interface IStreamReader 
    {
        void CreateReader(string path);
        string ReadLine();
        void DisposeReader();
    }
}
