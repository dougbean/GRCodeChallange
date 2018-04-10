using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System;
using GRLibrary;

namespace GRUnitTest
{
    [TestClass]
    public class ParserUnitTest
    {
        private IParser _parserService;

        [TestInitialize]
        public void Initialize()
        {
            List<FileFormatGetter> typeGetters = new List<FileFormatGetter>()
                  { new CommaFormatGetter(), new PipeFormatGetter(), new SpaceFormatGetter() };

            Dictionary<FileFormatEnum, char> delimiters = new Dictionary<FileFormatEnum, char>();

            delimiters.Add(FileFormatEnum.comma, ',');
            delimiters.Add(FileFormatEnum.pipe, '|');
            delimiters.Add(FileFormatEnum.space, ' ');

            _parserService = new ParserService(typeGetters, delimiters);
        }

        [TestMethod]
        public void ServiceShouldGetCommaFileTypeFromFileName()
        {
            //arrange
            string fileName = @"C:\gtr\gtr-comma.txt";

            //act          
            FileFormatEnum actual = _parserService.GetFileFormat(fileName); 

            //assert
            FileFormatEnum expected = FileFormatEnum.comma;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ServiceShouldGetPipeFileTypeFromFileName()
        {
            //arrange
            string fileName = @"C:\gtr\gtr-pipe.txt";

            //act            
            FileFormatEnum actual = _parserService.GetFileFormat(fileName);

            //assert
            FileFormatEnum expected = FileFormatEnum.pipe;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ServiceShouldGetSpaceFileTypeFromFileName()
        {
            //arrange
            string fileName = @"C:\gtr\gtr-space.txt";

            //act          
            FileFormatEnum actual = _parserService.GetFileFormat(fileName);

            //assert
            FileFormatEnum expected = FileFormatEnum.space;
            Assert.AreEqual(expected, actual);
        }

        //-------------------
        //[TestMethod]
        //public void DoSomething()
        //{
        //    //arrange
        //    string fileName = @"C:\gtr\gtr-comma.txt";

        //    //act          
        //    FileFormatEnum actual = _parserService.GetFileFormat(fileName);
        //    //I need code that gets the delimeter that corresponds to the enum returned.
           
        //    char delimiter = ',';
        //    delimiter = '|';
        //    delimiter = ' ';
        //    //Should this be a public property on the service and injected in?
        //    Dictionary<FileFormatEnum, char> delimiters = new Dictionary<FileFormatEnum, char>();
        //    delimiters.Add(FileFormatEnum.comma, ',');
        //    delimiters.Add(FileFormatEnum.pipe, '|');
        //    delimiters.Add(FileFormatEnum.space, ' ');

        //    KeyValuePair<FileFormatEnum, char> kvp = (from d in delimiters
        //                                             where d.Key == actual
        //                                             select d).FirstOrDefault();
        //    Console.WriteLine(kvp.Value);

        //    _parserService.ReadFile(fileName, kvp.Value);

        //    //assert
        //    FileFormatEnum expected = FileFormatEnum.comma;
        //    Assert.AreEqual(expected, actual);
        //}
        //---------------------
    }
}
