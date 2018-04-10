using System;
using System.Collections.Generic;
using System.Linq;
using GRLibrary.Model;

namespace GRLibrary
{    
    public class ParserService : IParser
    {
        private List<FileFormatGetter> _formatGetters;
        private Dictionary<FileFormatEnum, char> _delimiters;
        public ParserService(List<FileFormatGetter> formatGetters, Dictionary<FileFormatEnum, char> delimiters)
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
       
        public FileFormatEnum GetFileFormat(string fileName) 
        {
            var result = new FileFormatEnum();
            foreach (var getter in _formatGetters)
            {
                result = getter.GetFileFormat(fileName);
                if (result != FileFormatEnum.none)
                {
                    break;
                }
            }
            return result;
        }

        public IList<Person> ReadFile(string fileName)
        {
            List<Person> persons = new List<Person>();
            try
            {
                FileFormatEnum fileFormat = GetFileFormat(fileName);

                KeyValuePair<FileFormatEnum, char> kvp = GetDilimiter(fileFormat);

                persons = ReadFile(fileName, kvp.Value);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }            
            return persons;           
        }

        private KeyValuePair<FileFormatEnum, char> GetDilimiter(FileFormatEnum fileFormat)
        {
            return (from d in _delimiters
                    where d.Key == fileFormat
                    select d).FirstOrDefault();
        }
                
        private List<Person> ReadFile(string path, char delimiter)
        {
            var persons = new List<Person>();

            string line;
            using (StreamReader)
            {
                StreamReader.InitializeReader(path);
               
                while ((line = StreamReader.ReadLine()) != null)
                {                   
                    string[] parsedRecord = line.Split(delimiter);
                    Person person = GetPerson(parsedRecord);
                    persons.Add(person);
                }
            }

            return persons;
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
    }
}
