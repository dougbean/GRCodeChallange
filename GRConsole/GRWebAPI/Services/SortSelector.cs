using System.Collections.Generic;
using GRLibrary.Model;

namespace GRWebAPI.Services
{
    public abstract class SortSelector
    {        
        public abstract IList<Person> GetGersons(IList<Person> unsortedList, string sortBy);
    }
}
