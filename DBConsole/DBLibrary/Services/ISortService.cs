using System.Collections.Generic;
using DBLibrary.Model;

namespace DBLibrary.Services
{
    public interface ISortService
    {
        IList<Person> SortByGenderAndLastNameAscending(IList<Person> persons);
        IList<Person> SortByBirthDateAscending(IList<Person> persons);
        IList<Person> SortByLastNameDescending(IList<Person> persons);        
    }
}
