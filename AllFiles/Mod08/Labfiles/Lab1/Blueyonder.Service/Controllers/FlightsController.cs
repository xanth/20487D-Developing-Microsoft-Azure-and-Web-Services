using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Blueyonder.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

       
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            throw new Exception($"Blueyonder don't have a flight with this id = {id}");
        }
     
    }
}
