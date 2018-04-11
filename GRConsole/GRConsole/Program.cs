using System;
using System.Collections.Generic;
using GRLibrary;
using GRLibrary.Model;
using GRLibrary.Services;

namespace GRConsole
{
    class Program
    {
        private static IParser _parserService;
        private static ISortService _sortService; 

        static void Main(string[] args)
        {
            InitializeServices();

            while (true)
            {
                Parse(_parserService, _sortService);
            }
        }

        private static void InitializeServices()
        {
            List<FileFormatGetter> formatGetters = GetFormatGetters();
            Dictionary<FileFormatEnum, char> delimiters = GetDelimiters();
            _parserService = new ParserService(formatGetters, delimiters);
            _sortService = new SortService();
        }

        private static List<FileFormatGetter> GetFormatGetters()
        {
            return new List<FileFormatGetter>()
                  { new CommaFormatGetter(), new PipeFormatGetter(), new SpaceFormatGetter() };
        }

        private static Dictionary<FileFormatEnum, char> GetDelimiters()
        {
            Dictionary<FileFormatEnum, char> delimiters = new Dictionary<FileFormatEnum, char>();
            delimiters.Add(FileFormatEnum.comma, ',');
            delimiters.Add(FileFormatEnum.pipe, '|');
            delimiters.Add(FileFormatEnum.space, ' ');
            return delimiters;
        }

        private static void Parse(IParser _parserService, ISortService sortService)
        {
            Console.WriteLine("Enter file and path with file format specified as part of the name, " +
                           "such as 'comma', 'pipe' or 'space':");
            string input = Console.ReadLine();

            IList<Person> unsortedList = _parserService.GetPersons(input);
            Sort(sortService, unsortedList);
        }

        private static void Sort(ISortService sortService, IList<Person> unsortedList)
        {
            SortByGenderAndLastNameAscending(sortService, unsortedList);
            SortByBirthDateAscending(sortService, unsortedList);
            SortByLastNameDescending(sortService, unsortedList);
        }

        private static void SortByLastNameDescending(ISortService sortService, IList<Person> unsortedList)
        {
            IList<Person> sortedBylastName = sortService.SortByLastNameDescending(unsortedList);
            Console.WriteLine(" ");
            Console.WriteLine("Sorted by last name descending.");
            PrintList(sortedBylastName);
        }

        private static void SortByBirthDateAscending(ISortService sortService, IList<Person> unsortedList)
        {
            IList<Person> sortedByBirthdate = sortService.SortByBirthDateAscending(unsortedList);
            Console.WriteLine(" ");
            Console.WriteLine("Sorted by date of birth ascending.");
            PrintList(sortedByBirthdate);
        }

        private static void SortByGenderAndLastNameAscending(ISortService sortService, IList<Person> unsortedList)
        {
            IList<Person> sortedByGender = sortService.SortByGenderAndLastNameAscending(unsortedList);
            Console.WriteLine(" ");
            Console.WriteLine("Sorted by gender and by last name ascending.");
            PrintList(sortedByGender);
        }       

        private static void PrintList(IList<Person> persons)
        {
            foreach (var item in persons)
            {
                Console.WriteLine("{0} {1} {2} {3} {4}",
                    item.LastName,
                    item.FirstName,
                    item.Gender,
                    item.FavoriteColor,
                    item.DateOfBirth.ToString("d"));
            }
        }
    }
}
