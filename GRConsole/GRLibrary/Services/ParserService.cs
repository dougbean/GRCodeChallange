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
        public IStreamReader StreamReader {
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
       
        public FormatEnum GetFormat(string input) 
        {
            if (AreFormatGettersMissing())
            {
                throw new FormatGetterException("FileFormatGetters are missing.");
            }

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

                KeyValuePair<FormatEnum, char> delimiter = GetDilimiter(format);

                persons = GetPersons(fileName, delimiter.Value);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }            
            return persons;           
        }

        private KeyValuePair<FormatEnum, char> GetDilimiter(FormatEnum fileFormat)
        {
            if (AreDelimitersMissing())
            {
                throw new DelimitersException("Delimiters are missing.");
            }

            return (from d in _delimiters
                    where d.Key == fileFormat
                    select d).FirstOrDefault();
        }

        private bool AreDelimitersMissing()
        {
            return (_delimiters == null || _delimiters.Count == 0);
        }

        private List<Person> GetPersons(string path, char delimiter)
        {
            var persons = new List<Person>();

            string line;
            using (StreamReader)
            {
                StreamReader.InitializeReader(path);
                ReadLines(delimiter, persons);
            }

            return persons;
        }

        private void ReadLines(char delimiter, List<Person> persons)
        {
            string line;
            while ((line = StreamReader.ReadLine()) != null)
            {
                string[] parsedRecord = line.Split(delimiter);
                Person person = GetPerson(parsedRecord);
                persons.Add(person);
            }
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
