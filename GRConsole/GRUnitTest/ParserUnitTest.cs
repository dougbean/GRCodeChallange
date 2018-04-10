using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using GRLibrary;
using GRLibrary.Model;
using Moq;

namespace GRUnitTest
{
    [TestClass]
    public class ParserUnitTest
    {        
        private ParserService _parserService;

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
        public void ServiceShouldGetCommaFileFormatFromFileName()
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
        public void ServiceShouldGetPipeFileFormatFromFileName()
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
        public void ServiceShouldGetSpaceFileFormatFromFileName()
        {
            //arrange
            string fileName = @"C:\gtr\gtr-space.txt";

            //act          
            FileFormatEnum actual = _parserService.GetFileFormat(fileName);

            //assert
            FileFormatEnum expected = FileFormatEnum.space;
            Assert.AreEqual(expected, actual);
        }       

        [TestMethod]
        public void ParserServiceShouldGetPersonFromCommaDelimitedLine()
        {
            //arrange
            //the "comma" string in the file name tells the parser to use a comma delimiter
            string fileName = @"C:\gtr\gtr-comma.txt"; 

            var mockStreamReader = new Mock<IStreamReader>();            
            mockStreamReader.Setup(s => s.ReadLine())
                   .Returns(new Queue<string>(new[] { "Gibbe,Candace,Female,Crimson,3/28/2010", null }).Dequeue);
            
            mockStreamReader.Setup(s => s.InitializeReader(It.IsAny<String>())).Verifiable();
            _parserService.StreamReader = mockStreamReader.Object;

            //act
            IList<Person> persons = _parserService.GetPersons(fileName);
            var person = persons.FirstOrDefault();

            //assert
            string expected = "Gibbe";           
            Assert.AreEqual(expected, person.LastName);           
        }

        [TestMethod]
        public void ParserServiceShouldCorrectlyMapPersonProperties()
        {
            //arrange
            //the "comma" string in the file name tells the parser to use a comma delimiter
            string fileName = @"C:\gtr\gtr-comma.txt";

            string lastName = "Whiteside";
            string firstName = "Zachary";
            string gender = "Male";
            string favoriteColor = "Teal";
            DateTime dateOfBirth = DateTime.Parse("5/25/1977");

            var builder = new StringBuilder();
            builder.Append(lastName).Append(",");
            builder.Append(firstName).Append(",");
            builder.Append(gender).Append(",");
            builder.Append(favoriteColor).Append(",");
            builder.Append(dateOfBirth);

            var mockStreamReader = new Mock<IStreamReader>();
            mockStreamReader.Setup(s => s.ReadLine())
                   .Returns(new Queue<string>(new[] { builder.ToString(), null }).Dequeue);

            mockStreamReader.Setup(s => s.InitializeReader(It.IsAny<String>())).Verifiable();
            _parserService.StreamReader = mockStreamReader.Object;

            //act
            IList<Person> persons = _parserService.GetPersons(fileName);
            var person = persons.FirstOrDefault();

            //assert            
            Assert.AreEqual(lastName, person.LastName);
            Assert.AreEqual(firstName, person.FirstName);
            Assert.AreEqual(gender, person.Gender);
            Assert.AreEqual(favoriteColor, person.FavoriteColor);
            Assert.AreEqual(dateOfBirth, person.DateOfBirth);
        }

        [TestMethod]
        public void ParserServiceShouldGetPersonFromPipeDelimitedLine()
        {
            //arrange
            //the "pipe" string in the file name tells the parser to use a pipe delimiter
            string fileName = @"C:\gtr\gtr-pipe.txt";

            var mockStreamReader = new Mock<IStreamReader>();
            mockStreamReader.Setup(s => s.ReadLine())
                 .Returns(new Queue<string>(new[] { "Veregan|Jsandye|Female|Khaki|1/27/2007", null }).Dequeue);

            mockStreamReader.Setup(s => s.InitializeReader(It.IsAny<String>())).Verifiable();
            _parserService.StreamReader = mockStreamReader.Object;

            //act
            IList<Person> persons = _parserService.GetPersons(fileName);
            var person = persons.FirstOrDefault();

            //assert
            string expected = "Veregan";
            Assert.AreEqual(expected, person.LastName);
        }
                
        [TestMethod]
        public void PersonListCountShouldMatchLineNumberCount()
        {
            //arrange
            //the "pipe" string in the file name tells the parser to use a pipe delimiter
            string fileName = @"C:\gtr\gtr-pipe.txt";

            string line = "Veregan|Jsandye|Female|Khaki|1/27/2007";
            string line2 = "Ruperto|Billie|Female|Teal|7/24/1962";

            var mockStreamReader = new Mock<IStreamReader>();
            mockStreamReader.Setup(s => s.ReadLine())
                 .Returns(new Queue<string>(new[] { line, line2, null }).Dequeue);

            mockStreamReader.Setup(s => s.InitializeReader(It.IsAny<String>())).Verifiable();
            _parserService.StreamReader = mockStreamReader.Object;

            //act
            IList<Person> persons = _parserService.GetPersons(fileName);

            //assert
            int expected = 2;
            Assert.AreEqual(expected, persons.Count());
        }

        [TestMethod]
        public void ParserServiceShouldGetPersonFromSpaceDelimitedLine()
        {
            //arrange
            //the "space" string in the file name tells the parser to use a space delimiter
            string fileName = @"C:\gtr\gtr-space.txt";

            var mockStreamReader = new Mock<IStreamReader>();
            mockStreamReader.Setup(s => s.ReadLine())
                 .Returns(new Queue<string>(new[] { "Rout Theodora Female Teal 2/3/1976", null }).Dequeue);

            mockStreamReader.Setup(s => s.InitializeReader(It.IsAny<String>())).Verifiable();
            _parserService.StreamReader = mockStreamReader.Object;

            //act
            IList<Person> persons = _parserService.GetPersons(fileName);
            var person = persons.FirstOrDefault();

            //assert
            string expected = "Rout";
            Assert.AreEqual(expected, person.LastName);
        }
    }
}
