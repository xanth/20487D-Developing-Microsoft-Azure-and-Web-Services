using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlueYonder.Flights.DAL.Repository;
using BlueYonder.Flights.DAL.Models;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace BlueYonder.Flights.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private IFlightsRepository _flightsRepository;
        

        public FlightsController(IFlightsRepository flightsRepository)
        {
           
            _flightsRepository = flightsRepository;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Flight>> Get()
        {
            return Ok(_flightsRepository.GetAllFlights());
        }

        // GET api/values/5
        [HttpGet("{source}/{destination}/{date}")]
        public ActionResult<string> Get(string source,string destination,DateTime date)
        {

            var result = _flightsRepository.GetFlightByDate(source,destination,date);
            if (result == null)
                return NotFound();
            return Ok(result);
            
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
