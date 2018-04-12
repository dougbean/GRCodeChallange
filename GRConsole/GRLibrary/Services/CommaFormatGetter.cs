using GRLibrary.Model;

namespace GRLibrary.Services
{
    public class CommaFormatGetter : FileFormatGetter
    {
        public override FormatEnum GetFileFormat(string fileName)
        {
            FormatEnum result = new FormatEnum();
            if (fileName.Contains(Constants.Comma))
            {
                result = FormatEnum.comma;
            }
            return result;
        }
    }
}
