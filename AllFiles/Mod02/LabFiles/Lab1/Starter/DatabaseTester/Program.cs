using System;
using System.Linq;
using DAL.Database;

namespace DatabaseTester
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new MyDbContext())
            {
                DbInitializer.Initialize(context);

                var hotel = context.Hotels.FirstOrDefault();
                Console.WriteLine($"hotel name: {hotel?.Name ?? "N/A"}");

                Console.WriteLine("Rooms:");
                foreach (var room in context.Rooms.ToList())
                    Console.WriteLine($"room number: {room.Number}, Price: {room.Price}");

                Console.WriteLine("Travelers:");
                foreach (var traveler in context.Travelers.ToList())
                    Console.WriteLine($"traveler name: {traveler.Name}, email: {traveler.Email} ");
            }
        }
    }
}
