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
using GRWebAPI.Services;

namespace GRWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Records")]
    public class RecordsController : Controller
    {
        private ParserServiceWrapper _parserServiceWrapper = ParserServiceWrapper.GetInstance();
        
        private ISortService _sortService; 

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
            FormatEnum format = _parserServiceWrapper.ParserService.GetFormat(record.Delimiter);
            Person person = _parserServiceWrapper.ParserService.GetPerson(format, record.Line);
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
