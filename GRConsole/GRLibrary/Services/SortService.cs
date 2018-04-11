using System;
using System.Collections.Generic;
using System.Linq;
using GRLibrary.Model;

namespace GRLibrary.Services
{
    public class SortService : ISortService
    {
        public IList<Person> SortByGenderAndLastNameAscending(IList<Person> persons)
        {
            var sortedList = (from p in persons
                              orderby p.Gender, p.LastName ascending
                              select p).ToList();
            return sortedList;
        }

        public IList<Person> SortByBirthDateAscending(IList<Person> persons)
        {
            var sortedList = (from p in persons
                              orderby p.DateOfBirth ascending
                              select p).ToList();
            return sortedList;
        }

        public IList<Person> SortByLastNameDescending(IList<Person> persons)
        {
            var sortedList = (from p in persons
                              orderby p.LastName descending
                              select p).ToList();
            return sortedList;
        }
    }
}
