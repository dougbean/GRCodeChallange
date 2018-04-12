using GRLibrary.Model;

namespace GRLibrary.Services
{
    public class PipeFormatGetter : FileFormatGetter
    {
        public override FormatEnum GetFileFormat(string fileName)
        {
            FormatEnum result = new FormatEnum();
            if (fileName.Contains(Constants.Pipe))
            {
                result = FormatEnum.pipe;
            }
            return result;
        }
    }
}
