using System.Collections.Generic;
using DBLibrary.Model;

namespace DBWebAPI.Services
{
    public abstract class SortSelector
    {        
        public abstract IList<Person> GetGersons(IList<Person> unsortedList, string sortBy);
    }
}
