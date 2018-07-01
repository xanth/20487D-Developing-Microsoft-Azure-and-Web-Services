using System;
using Microsoft.EntityFrameworkCore;
using BlueYonder.Flights.Service.Models;

namespace BlueYonder.Flights.Service.Database
{
    public class FlightContext : DbContext
    {
        public FlightContext()
        {

        }
		
        public FlightContext(DbContextOptions<FlightContext> options)
        : base(options)
        {

        }
		
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConnectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_dbConnectionString");

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(dbConnectionString);
            }
        }
		
        public DbSet<Flight> Flights { get; set; }
    }
}