using System;
using System.Collections.Generic;
using System.Text;
using BlueYonder.Flights.DAL.Models;
using BlueYonder.Flights.DAL.Database;
using System.Linq;

namespace BlueYonder.Flights.DAL.Repository
{
    public class FlightsRepository : IFlightsRepository
    {
        public IEnumerable<Flight> GetAllFlights()
        {
            using (FlightsContext context = new FlightsContext())
            {
                return context.Flight.ToList();
            }
        }

        public IEnumerable<Flight> GetFlightByDate(string source, string destination, DateTime date)
        {
            using (FlightsContext context = new FlightsContext())
            {
                return context.Flight.Where(f => f.Source == source && f.Destination == destination
                                         && f.DepartureTime.Date == date.Date).ToList();
            }
        }
        
    }
}
