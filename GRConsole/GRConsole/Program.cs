using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using GRLibrary;

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

            parserService.ReadFile(fileName);

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
