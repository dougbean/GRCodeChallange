using System.Collections.Generic;
using GRWebAPI.Model;
using GRLibrary.Services;
using GRLibrary.Model;

namespace GRWebAPI.Services
{
    public class NameSort : SortSelector
    {
        private ISortService _sortService;
        public NameSort(ISortService sortService)
        {
            _sortService = sortService;
        }      

        public override IList<Person> GetGersons(IList<Person> unsortedList, string sortBy)
        {
            IList<Person> persons = new List<Person>();
            if (sortBy.Contains(Constants.Name))
            {
                persons = _sortService.SortByLastNameDescending(unsortedList);
            }
            return persons;
        }
    }
}
