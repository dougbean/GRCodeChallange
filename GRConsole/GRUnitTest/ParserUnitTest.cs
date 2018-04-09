using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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

            _parserService = new ParserService(typeGetters);
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
    }
}
