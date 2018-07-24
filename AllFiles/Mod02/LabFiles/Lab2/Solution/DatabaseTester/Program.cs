using System;
using DAL.Database;
using DAL.Models;
using System.Linq;
using System.Collections.Generic;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DatabaseTester
{
    class Program
    {
        static HotelBookingRepository repo = new HotelBookingRepository();

        static async Task Main(string[] args)
        {
                Console.WriteLine("Before adding a new booking");
                PrintBookings();

                Booking newBooking = await repo.Add(1, 1, DateTime.Now.AddDays(6), 12284);
                Console.WriteLine("After adding a new booking");
                PrintBookings();

                // Updating the booking was paid.
                newBooking.Paid = true;
                Booking updatedBooking = await repo.Update(newBooking);
                Console.WriteLine("After updeting booking");
                PrintBookings();

                repo.Delete(updatedBooking.BookingId);
                Console.WriteLine("After deleting the new booking");
                PrintBookings();

                Console.ReadKey();
        }

        static void PrintBookings()
        {
            IEnumerable<Booking> bookings = repo.GetAllByTravelerId(1);

            foreach (Booking booking in bookings)
            {
                Console.WriteLine($"booking info - Traveler Name {booking.Traveler.Name}, CheckIn {booking.CheckIn}, Room number {booking.Room}, Guests {booking.Guests}, IsPaid {booking.Paid}");
            }
            Console.WriteLine();
        }
    }
}
