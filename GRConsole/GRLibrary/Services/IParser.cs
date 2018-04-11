using System.Collections.Generic;
using GRLibrary.Model;

namespace GRLibrary.Services
{
    public interface IParser
    {
        FormatEnum GetFormat(string input);
        IList<Person> GetPersons(string fileName);
        Person GetPerson(FormatEnum format, string record);
    }
}
