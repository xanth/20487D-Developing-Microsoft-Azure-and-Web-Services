using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlueYonder.Flights.DAL.Repository;
using BlueYonder.Flights.DAL.Models;
using StackExchange.Redis;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace BlueYonder.Flights.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly string _redisConnectionString;
        
        public FlightsController(IConfiguration configuration,IFlightsRepository flightsRepository)
        {
            _flightsRepository = flightsRepository;
            _redisConnectionString = configuration["RedisConnectionString"];
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
            var key = source + destination + date.Date.ToShortDateString();

            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(_redisConnectionString);
            IDatabase redisDB = connection.GetDatabase();

            var cacheResult = redisDB.StringGet(key);
            if (!cacheResult.HasValue)
            {
                var result = _flightsRepository.GetFlightByDate(source, destination, date);
                if (result == null)
                    return NotFound();
                redisDB.StringSet(key,JsonConvert.SerializeObject(result));
                return Ok(result);
            }
            Request.HttpContext.Response.Headers.Add("X-Cache","true");
            return Ok(cacheResult.ToString());
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
