using System;
using System.Security.Cryptography;
using System.Text;
using BlueYonder.Hotels.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlueYonder.Hotels.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "OK";
        }
    }
}