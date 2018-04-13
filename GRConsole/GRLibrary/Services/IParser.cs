using System.Collections.Generic;
using DBLibrary.Model;

namespace DBLibrary.Services
{
    public interface IParser
    {
        FormatEnum GetFormat(string input);
        IList<Person> GetPersons(string fileName);
        Person GetPerson(FormatEnum format, string record);
    }
}
