using GRLibrary.Model;

namespace GRLibrary.Services
{
    public class SpaceFormatGetter : FileFormatGetter
    {
        public override FormatEnum GetFileFormat(string fileName)
        {
            FormatEnum result = new FormatEnum();
            if (fileName.Contains(Constants.Space))
            {
                result = FormatEnum.space;
            }
            return result;
        }
    }
}
