using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueYonder.Flights.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlueYonder.Flights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FlightsContext context;

        public FlightsController(FlightsContext context)
        {
            this.context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Flight>> Get() => context.Flights.ToList();

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Flight flight)
        {
            context.Flights.Add(flight);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), flight.Id);
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
