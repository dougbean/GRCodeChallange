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
    //[Route("api/Records")] //todo:remove this.
    [Route("Records")]
    public class RecordsController : Controller
    {
        private ParserServiceWrapper _parserServiceWrapper = ParserServiceWrapper.GetInstance();
        private SortServiceWrapper _sortServiceWrapper = SortServiceWrapper.GetInstance();

        //lazy load the sort selectors
        private List<SortSelector> _sortSelectors;
        private List<SortSelector> SortSelectors
        {
            get
            {
                if (_sortSelectors == null)
                {
                    _sortSelectors = new List<SortSelector>(){
                            new GenderSort(_sortServiceWrapper.SortService),
                            new BirthdateSort(_sortServiceWrapper.SortService),
                            new NameSort(_sortServiceWrapper.SortService)};
                }
                return _sortSelectors;
            }
        }

        //[HttpGet]
        //public IList<Person> Get() //todo: remember to have the sorting service format the date of the result.
        //{
        //    return _parserServiceWrapper.PersonCache;            
        //}        

        [HttpGet("{sortby}")]
        public IList<Person> Get(string sortby)
        {
            IList<Person> unsortedList = _parserServiceWrapper.PersonCache;           
            IList<Person> sortedList = GetPersons(unsortedList, sortby);           
            return sortedList;
        }

        private IList<Person> GetPersons(IList<Person> unsortedList, string sortBy)
        {
            IList<Person> persons = new List<Person>();
            foreach (var selector in SortSelectors)
            {
                persons = selector.GetGersons(unsortedList, sortBy);
                if (persons.Any())
                {
                    break;
                }
            }
            return persons;
        }


        //// GET: api/Records/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}      

        // POST: api/Records
        [HttpPost]
        public void Post([FromBody]Record record)
        {
            FormatEnum format = _parserServiceWrapper.ParserService.GetFormat(record.Delimiter);
            Person person = _parserServiceWrapper.ParserService.GetPerson(format, record.Line);
            _parserServiceWrapper.PersonCache.Add(person);
        }

     

        //// PUT: api/Records/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
