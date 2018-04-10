using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using GRLibrary;
using GRLibrary.Model;

namespace GRConsole
{
    class Program
    {
       static void Main(string[] args)
        {
            //string[] myArgs = new string[1];
            //myArgs[0] = @"C:\gtr";
            //ReadDirectory(myArgs);

            //_parserService.ReadFile()

            List<FileFormatGetter> typeGetters = new List<FileFormatGetter>()
                  { new CommaFormatGetter(), new PipeFormatGetter(), new SpaceFormatGetter() };

            Dictionary<FileFormatEnum, char> delimiters = new Dictionary<FileFormatEnum, char>();

            delimiters.Add(FileFormatEnum.comma, ',');
            delimiters.Add(FileFormatEnum.pipe, '|');
            delimiters.Add(FileFormatEnum.space, ' ');

            IParser parserService = new ParserService(typeGetters, delimiters);
            string fileName = @"C:\gtr\gtr-comma.txt";

            IList<Person> persons = parserService.GetPersons(fileName);
            foreach (var person in persons)
            {
                Console.WriteLine("{0} {1} {2} {3} {4}",
                    person.FirstName,
                    person.LastName,
                    person.Gender,
                    person.FavoriteColor,
                    person.DateOfBirth.ToString("d"));
            }

            Console.WriteLine("sorted by first name...");

            var query = (from p in persons
                         orderby p.FirstName
                         select p);
            foreach (var item in query)
            {
                Console.WriteLine("{0} {1} {2} {3} {4}",
                   item.FirstName,
                   item.LastName,
                   item.Gender,
                   item.FavoriteColor,
                   item.DateOfBirth.ToString("d"));
            }

            Console.WriteLine("sorted by date of birth...");

            var byDateOfBirthQuery = (from p in persons
                                      orderby p.DateOfBirth
                                      select p);

            foreach (var item in byDateOfBirthQuery)
            {
                Console.WriteLine("{0} {1} {2} {3} {4}",
                   item.FirstName,
                   item.LastName,
                   item.Gender,
                   item.FavoriteColor,
                   item.DateOfBirth.ToString("d"));
            }

            Console.WriteLine("press any key.");
            Console.ReadKey();
        }

        private static void ReadDirectory(string[] args)
        {
            string path = args[0];
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                Console.WriteLine(file);                
            }
        }
    }
}
