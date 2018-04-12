using GRLibrary.Model;

namespace GRLibrary.Services
{
    public abstract class FileFormatGetter
    {
        public abstract FormatEnum GetFileFormat(string fileName);
    }
}
