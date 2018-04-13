using DBLibrary.Model;

namespace DBLibrary.Services
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
