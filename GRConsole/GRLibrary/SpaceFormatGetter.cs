using System;
using System.Collections.Generic;
using System.Text;

namespace GRLibrary
{
    public class SpaceFormatGetter : FileFormatGetter
    {
        public override FileFormatEnum GetFileFormat(string fileName)
        {
            FileFormatEnum result = new FileFormatEnum();
            if (fileName.Contains(Constants.Space))
            {
                result = FileFormatEnum.space;
            }
            return result;
        }
    }
}
