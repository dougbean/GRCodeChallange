using System;
using System.Collections.Generic;

namespace GRLibrary
{
    public class ParserService : IParser
    {
        private List<FileFormatGetter> _formatGetters;
        public ParserService(List<FileFormatGetter> formatGetters)
        {
            _formatGetters = formatGetters;
        }

        public FileFormatEnum GetFileFormat(string fileName)
        {
            var result = new FileFormatEnum();
            foreach (var getter in _formatGetters)
            {
                result = getter.GetFileFormat(fileName);
                if (result != FileFormatEnum.none)
                {
                    break;
                }
            }
            return result;
        }
    }
}
