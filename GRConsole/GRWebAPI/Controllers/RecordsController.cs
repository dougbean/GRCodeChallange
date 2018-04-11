using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GRLibrary.Model;
using GRLibrary.Services;
using GRLibrary;
using GRWebAPI.Model;

namespace GRWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Records")]
    public class RecordsController : Controller
    {
        private IParser _parserService;
        private ISortService _sortService; 
                        
        public IParser ParserService
        {
            get
            {
                if (_parserService == null)
                {
                    InitializeParserService();
                }
                return _parserService;
            }          
        }

        private void InitializeParserService()
        {
            List<FileFormatGetter> formatGetters = GetFormatGetters();
            Dictionary<FormatEnum, char> delimiters = GetDelimiters();
            _parserService = new ParserService(formatGetters, delimiters);            
        }

        private List<FileFormatGetter> GetFormatGetters()
        {
            return new List<FileFormatGetter>()
                  { new CommaFormatGetter(), new PipeFormatGetter(), new SpaceFormatGetter() };
        }

        private Dictionary<FormatEnum, char> GetDelimiters()
        {
            Dictionary<FormatEnum, char> delimiters = new Dictionary<FormatEnum, char>();
            delimiters.Add(FormatEnum.comma, ',');
            delimiters.Add(FormatEnum.pipe, '|');
            delimiters.Add(FormatEnum.space, ' ');
            return delimiters;
        }

        public ISortService SortService
        {
            get
            {
                if (_sortService == null)
                {
                    _sortService = new SortService(); 
                }
                return _sortService;
            }
        }

        // GET: api/Records
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Records/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        //// POST: api/Records
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{

        //}      

        // POST: api/Records
        [HttpPost]
        public void Post([FromBody]Record record)
        {
            FormatEnum format = ParserService.GetFormat(record.Delimiter);
            Person person = ParserService.GetPerson(format, record.Line);
        }

        // PUT: api/Records/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
