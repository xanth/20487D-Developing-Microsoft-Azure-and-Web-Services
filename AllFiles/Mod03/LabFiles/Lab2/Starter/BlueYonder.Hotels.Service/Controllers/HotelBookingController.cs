using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlueYonder.Hotels.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
       [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Hotel1", "Hotel2" };
        }

        // POST api/HotelBooking
        [HttpPost]
        public ActionResult<string> Post(string someData)
        {
            return "Post request succeded";
        }

        // PUT api/HotelBooking
        [HttpPut]
        public ActionResult<string> Put(string someData)
        {
            return "Put request succeded";
        }
    }
}
