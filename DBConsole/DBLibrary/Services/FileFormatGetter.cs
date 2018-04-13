using DBLibrary.Model;

namespace DBLibrary.Services
{
    public abstract class FileFormatGetter
    {
        public abstract FormatEnum GetFileFormat(string fileName);
    }
}
