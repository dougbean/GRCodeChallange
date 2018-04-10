
namespace GRLibrary
{
    public interface IParser
    {
        FileFormatEnum GetFileFormat(string fileName);
        void ReadFile(string fullFileName); 
    }
}
