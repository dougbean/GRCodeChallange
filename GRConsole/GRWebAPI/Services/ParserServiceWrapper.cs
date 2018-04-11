using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GRLibrary.Model;
using GRLibrary.Services;
using GRLibrary;
using GRWebAPI.Model;

namespace GRWebAPI.Services
{
    public class ParserServiceWrapper
    {
        public IParser ParserService { get; set; }
        public IList<Person> PersonCache { get; set; }

        private ParserServiceWrapper()
        {
            InitializeParserService();
        }

        private static volatile ParserServiceWrapper _instance = null;
        public static ParserServiceWrapper GetInstance()
        {
            if (_instance == null)
            {
                lock (typeof(ParserServiceWrapper))
                {
                    _instance = new ParserServiceWrapper(); 
                }
            }
            return _instance;
        }

        private void InitializeParserService()
        {
            List<FileFormatGetter> formatGetters = GetFormatGetters();
            Dictionary<FormatEnum, char> delimiters = GetDelimiters();
            ParserService = new ParserService(formatGetters, delimiters);
        }

        private static List<FileFormatGetter> GetFormatGetters()
        {
            return new List<FileFormatGetter>()
                  { new CommaFormatGetter(), new PipeFormatGetter(), new SpaceFormatGetter() };
        }

        private static Dictionary<FormatEnum, char> GetDelimiters()
        {
            Dictionary<FormatEnum, char> delimiters = new Dictionary<FormatEnum, char>();
            delimiters.Add(FormatEnum.comma, ',');
            delimiters.Add(FormatEnum.pipe, '|');
            delimiters.Add(FormatEnum.space, ' ');
            return delimiters;
        }
    }
}

