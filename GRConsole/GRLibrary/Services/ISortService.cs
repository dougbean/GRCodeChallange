using System.Collections.Generic;
using GRLibrary.Model;

namespace GRLibrary.Services
{
    public interface ISortService
    {
        IList<Person> SortByGenderAndLastNameAscending(IList<Person> persons);
        IList<Person> SortByBirthDateAscending(IList<Person> persons);
        IList<Person> SortByLastNameDescending(IList<Person> persons);        
    }
}
