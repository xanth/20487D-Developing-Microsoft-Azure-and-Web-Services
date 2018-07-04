using System;
using System.Collections.Generic;

namespace BlueYonder.Flights.Service.Models
{
   public class Flight
   {
     public int Id { get; set; }
     public string Origin { get; set; }
     public string Destination { get; set; }
     public string FlightNumber { get; set; }
     public DateTime DepartureTime { get; set; }
	 public ICollection<Traveler> Travelers { get; set; }
   }
}
