using System.Collections.Generic;
using GRWebAPI.Model;
using GRLibrary.Services;
using GRLibrary.Model;

namespace GRWebAPI.Services
{
    public class GenderSort : SortSelector
    {
        private ISortService _sortService;
        public GenderSort(ISortService sortService)
        {
            _sortService = sortService;
        }
      
        public override IList<Person> GetGersons(IList<Person> unsortedList, string sortBy)
        {
            IList<Person> persons = new List<Person>();
            if (sortBy.Contains(GRWebAPI.Model.Constants.Gender))
            {
                persons = _sortService.SortByGenderAndLastNameAscending(unsortedList);
            }
            return persons;
        }
    }
}
