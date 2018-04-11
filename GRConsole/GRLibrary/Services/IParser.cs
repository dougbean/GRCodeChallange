using System.Collections.Generic;
using GRLibrary.Model;

namespace GRLibrary.Services
{
    public interface IParser
    {
        FileFormatEnum GetFileFormat(string fileName);
        IList<Person> GetPersons(string fullFileName); 
    }
}
