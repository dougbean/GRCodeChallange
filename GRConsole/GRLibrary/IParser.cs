using System.Collections.Generic;
using GRLibrary.Model;

namespace GRLibrary
{
    public interface IParser
    {
        FileFormatEnum GetFileFormat(string fileName);
        IList<Person> ReadFile(string fullFileName); 
    }
}
