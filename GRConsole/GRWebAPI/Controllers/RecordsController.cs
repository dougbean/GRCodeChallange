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
        private SortServiceWrapper _sortServiceWrapper = SortServiceWrapper.GetInstance();         

        [HttpGet]
        public IList<Person> Get() //todo: remember to have the sorting service format the date of the result.
        {
            return _parserServiceWrapper.PersonCache;            
        }

        // GET: api/Records/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }      

        // POST: api/Records
        [HttpPost]
        public void Post([FromBody]Record record)
        {
            FormatEnum format = _parserServiceWrapper.ParserService.GetFormat(record.Delimiter);
            Person person = _parserServiceWrapper.ParserService.GetPerson(format, record.Line);
            _parserServiceWrapper.PersonCache.Add(person);
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
