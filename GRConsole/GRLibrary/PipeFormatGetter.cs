
namespace GRLibrary
{
    public class PipeFormatGetter : FileFormatGetter
    {
        public override FileFormatEnum GetFileFormat(string fileName)
        {
            FileFormatEnum result = new FileFormatEnum();
            if (fileName.Contains(Constants.Pipe))
            {
                result = FileFormatEnum.pipe;
            }
            return result;
        }
    }
}
