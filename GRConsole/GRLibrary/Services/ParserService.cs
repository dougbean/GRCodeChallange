using System;
using System.Collections.Generic;
using System.Linq;
using GRLibrary.Model;
using GRLibrary.Wrappers;

namespace GRLibrary.Services
{    
    public class ParserService : IParser
    {
        private List<FileFormatGetter> _formatGetters;
        private Dictionary<FormatEnum, char> _delimiters;
        public ParserService(List<FileFormatGetter> formatGetters, Dictionary<FormatEnum, char> delimiters)
        {
            _formatGetters = formatGetters;
            _delimiters = delimiters;
        }

        private IStreamReader _streamReader;
        public IStreamReader StreamReaderWrapper {
           get 
            {
                if(_streamReader == null)
                {
                    _streamReader = new StreamReaderWrapper();
                }
                return _streamReader;
            }
            set
            {
                _streamReader = value; 
            }
        }

        private IFileSystem _fileSystem;
        public IFileSystem FileSystemWrapper
        {
            get
            {
                if (_fileSystem == null)
                {
                    _fileSystem = new FileSystemWrapper();
                }
                return _fileSystem;
            }
            set
            {
                _fileSystem = value;
            }
        }

        public FormatEnum GetFormat(string input)
        {            
            CheckFormatters();

            var result = new FormatEnum();
            foreach (var getter in _formatGetters)
            {
                result = getter.GetFileFormat(input);
                if (result != FormatEnum.none)
                {
                    break;
                }
            }
            return result;
        }

        private void CheckFormatters()
        {
            if (AreFormatGettersMissing())
            {
                throw new Exception("FileFormatGetters are missing.");
            }
        }

        private bool AreFormatGettersMissing()
        {
            return (_formatGetters == null || _formatGetters.Count == 0);            
        }      

        public IList<Person> GetPersons(string fileName)
        {
            List<Person> persons = new List<Person>();
            try
            {
                FormatEnum format = GetFormat(fileName);                 
                CheckFormat(format);               
                KeyValuePair<FormatEnum, char> delimiter = GetDilimiter(format);
                persons = GetPersons(fileName, delimiter.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }            
            return persons;           
        }

        private void CheckFormat(FormatEnum format)
        {
            if (IsSpecifiedFormatNone(format))
            {
                throw new Exception("Valid file format is not specified in file name.");
            }
        }

        private bool IsSpecifiedFormatNone(FormatEnum format)
        {
            return (FormatEnum.none == format);
        }

        private KeyValuePair<FormatEnum, char> GetDilimiter(FormatEnum fileFormat)
        {          
            CheckDelimiters();

            return (from d in _delimiters
                    where d.Key == fileFormat
                    select d).FirstOrDefault();
        }

        private void CheckDelimiters()
        {
            if (AreDelimitersMissing())
            {
                throw new Exception("Delimiters are missing.");
            }
        }

        private bool AreDelimitersMissing()
        {
            return (_delimiters == null || _delimiters.Count == 0);
        }

        private List<Person> GetPersons(string path, char delimiter)
        {
            var persons = new List<Person>();

            string line;
            using (StreamReaderWrapper)
            {                
                CheckForFile(path);
                StreamReaderWrapper.InitializeReader(path);
                ReadLines(delimiter, persons);
            }

            return persons;
        }

        private void CheckForFile(string path)
        {
            if (IsSpecifiedFileNotFound(path))
            {
                throw new Exception("Specified file is not found.");
            }
        }

        private bool IsSpecifiedFileNotFound(string path)
        {
            return (!FileSystemWrapper.FileExists(path));
        }

        private void ReadLines(char delimiter, List<Person> persons)
        {
            string line;
            while ((line = StreamReaderWrapper.ReadLine()) != null)
            {
                string[] parsedRecord = line.Split(delimiter);                
                CheckArraySize(parsedRecord);
                Person person = GetPerson(parsedRecord);
                persons.Add(person);
            }
        }

        private void CheckArraySize(string[] parsedRecord)
        {
            if (IsParsedRecordArrayWrongSize(parsedRecord))
            {
                throw new Exception("Parsing of record failed. Parsed record array does not have five elements.");
            }
        }

        private bool IsParsedRecordArrayWrongSize(string[] parsedRecord)
        {
            return (parsedRecord.Length != 5);
        }

        private static Person GetPerson(string[] parsedRecord)
        {
            var person = new Person()
            {
                LastName = parsedRecord[0],
                FirstName = parsedRecord[1],                
                Gender = parsedRecord[2],
                FavoriteColor = parsedRecord[3],
            };

            DateTime result;
            bool success = DateTime.TryParse(parsedRecord[4], out result);
            if (success)
            {
                person.DateOfBirth = result;
            }
            return person;
        }

        public Person GetPerson(FormatEnum format, string record)
        {
            Person person;
            try
            {
                KeyValuePair<FormatEnum, char> delimiter = GetDilimiter(format);

                string[] parsedRecord = record.Split(delimiter.Value);
                CheckArraySize(parsedRecord);
                person = GetPerson(parsedRecord);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return person;
        }
    }
}
