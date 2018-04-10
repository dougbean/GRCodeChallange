using System;
using System.Collections.Generic;
using System.Linq;

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

        public FileFormatEnum GetFileFormat(string fileName) //should this be a private method used internally?
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

        public void ReadFile(string fileName)
        {
            FileFormatEnum fileFormat = GetFileFormat(fileName);

            KeyValuePair<FileFormatEnum, char> kvp = GetDilimiter(fileFormat);

            ReadFile(fileName, kvp.Value);
        }

        private KeyValuePair<FileFormatEnum, char> GetDilimiter(FileFormatEnum fileFormat)
        {
            return (from d in _delimiters
                    where d.Key == fileFormat
                    select d).FirstOrDefault();
        }

        private void ReadFile(string fileName, char delimiter)
        {
            try
            {
                string line;
                //I can also pass in a stream to the StreamReaderWrapper
                using (IStreamReader streamReader = new StreamReaderWrapper(fileName))
                {                   
                    while (true)
                    {
                        line = streamReader.ReadLine();
                        if (line != null)
                        {
                            //Console.WriteLine(line);
                            string[] parsedRecord = line.Split(delimiter);
                            foreach (var field in parsedRecord)
                            {
                                Console.WriteLine(field.Trim());
                            }
                        }
                        else
                        {
                            break;
                        }                       
                    }
                }               
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
