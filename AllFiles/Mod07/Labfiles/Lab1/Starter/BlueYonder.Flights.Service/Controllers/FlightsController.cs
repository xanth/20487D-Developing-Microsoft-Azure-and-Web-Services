using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace BlueYonder.Flights.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {

        [HttpGet("FinalizeFlight")]
        public async Task<ActionResult> FinalizeFlight()
        {
            
            return Ok();
        }


        private System.IO.MemoryStream GeneratedManifests()
        {
            System.IO.MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            Random random = new Random();

            for (byte i = 0; i < 100; i++)
            {
                writer.Write(random.Next(10));
            }
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
