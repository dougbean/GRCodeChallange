
namespace GRLibrary
{
    public class CommaFormatGetter : FileFormatGetter
    {
        public override FileFormatEnum GetFileFormat(string fileName)
        {
            FileFormatEnum result = new FileFormatEnum();
            if (fileName.Contains(Constants.Comma))
            {
                result = FileFormatEnum.comma;
            }
            return result;
        }
    }
}
